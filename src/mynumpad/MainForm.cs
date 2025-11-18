using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Management;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using mynumpad.Config;
using Microsoft.Win32;

namespace mynumpad
{
    /// <summary>
    /// main form for keyboard mapper application
    /// handles keyboard hook, ui, and key mapping functionality
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants and Win32 Imports

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int KEYEVENTF_KEYUP = 0x0002;

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        #endregion

        #region Fields

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private Dictionary<int, Action> _shortcuts;
        private NotifyIcon _notifyIcon;
        private DataGridView _mappingsGrid;
        private Button _startButton;
        private Button _stopButton;
        private Button _saveButton;
        private Button _loadButton;
        private Button _resetButton;
        private Label _statusLabel;
        private CheckBox _minimizeToTrayCheckbox;
        private CheckBox _startWithWindowsCheckbox;
        private MenuStrip _menuStrip;
        private bool _isRunning = false;
        private AppSettings _settings;

        #endregion

        #region Constructor and Initialization

        public MainForm()
        {
            InitializeComponent();
            _shortcuts = new Dictionary<int, Action>();
            _settings = AppSettings.Load();
            SetupUI();
            SetupSystemTray();
            LoadSettingsIntoUI();

            // apply startup settings
            if (_settings.StartMinimized)
            {
                this.WindowState = FormWindowState.Minimized;
                if (_settings.MinimizeToTray)
                {
                    this.Hide();
                }
            }
        }

        /// <summary>
        /// sets up the system tray icon and context menu
        /// </summary>
        private void SetupSystemTray()
        {
            _notifyIcon = new NotifyIcon
            {
                // icon will be set if available
                Visible = false,
                Text = "MyNumpad Keyboard Mapper"
            };

            // create context menu for system tray
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Show", null, (s, e) => ShowMainWindow());
            contextMenu.Items.Add("Start Mapping", null, (s, e) => StartMapping());
            contextMenu.Items.Add("Stop Mapping", null, (s, e) => StopMapping());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("Exit", null, (s, e) => ExitApplication());

            _notifyIcon.ContextMenuStrip = contextMenu;
            _notifyIcon.DoubleClick += (s, e) => ShowMainWindow();
        }

        /// <summary>
        /// loads saved settings into the ui controls
        /// </summary>
        private void LoadSettingsIntoUI()
        {
            // load key mappings into grid
            _mappingsGrid.Rows.Clear();
            foreach (var mapping in _settings.KeyMappings)
            {
                _mappingsGrid.Rows.Add(mapping.NumpadKey, mapping.Shortcut, mapping.Description);
            }

            // load checkbox settings
            _minimizeToTrayCheckbox.Checked = _settings.MinimizeToTray;
            _startWithWindowsCheckbox.Checked = _settings.StartWithWindows;
        }

        /// <summary>
        /// saves current ui settings to configuration
        /// </summary>
        private void SaveSettingsFromUI()
        {
            _settings.KeyMappings.Clear();
            foreach (DataGridViewRow row in _mappingsGrid.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    _settings.KeyMappings.Add(new KeyMapping
                    {
                        NumpadKey = row.Cells[0].Value.ToString() ?? string.Empty,
                        Shortcut = row.Cells[1].Value.ToString() ?? string.Empty,
                        Description = row.Cells[2].Value?.ToString() ?? string.Empty
                    });
                }
            }

            _settings.MinimizeToTray = _minimizeToTrayCheckbox.Checked;
            _settings.StartWithWindows = _startWithWindowsCheckbox.Checked;

            // handle windows startup registry
            SetStartupRegistry(_settings.StartWithWindows);
        }

        #endregion

        #region UI Setup

        /// <summary>
        /// initializes and configures all ui components
        /// </summary>
        private void SetupUI()
        {
            this.Text = "MyNumpad Keyboard Mapper";
            this.Size = new Size(700, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // menu strip
            SetupMenuStrip();

            int yPos = 40;

            // target keyboard label
            Label hardwareIdLabel = new Label
            {
                Text = "Target Keyboard: " + _settings.TargetKeyboardId,
                Location = new Point(20, yPos),
                Size = new Size(640, 30),
                AutoSize = true,
                Font = new Font(this.Font.FontFamily, 8)
            };
            this.Controls.Add(hardwareIdLabel);
            yPos += 40;

            // mappings grid
            SetupMappingsGrid(ref yPos);

            // options section
            SetupOptionsSection(ref yPos);

            // buttons section
            SetupButtons(ref yPos);

            // status label
            _statusLabel = new Label
            {
                Text = "Status: Stopped",
                Location = new Point(20, yPos),
                AutoSize = true,
                ForeColor = Color.Red,
                Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold)
            };
            this.Controls.Add(_statusLabel);
        }

        /// <summary>
        /// sets up the application menu strip
        /// </summary>
        private void SetupMenuStrip()
        {
            _menuStrip = new MenuStrip();

            // file menu
            var fileMenu = new ToolStripMenuItem("File");
            fileMenu.DropDownItems.Add("Save Settings", null, (s, e) => SaveSettings());
            fileMenu.DropDownItems.Add("Load Settings", null, (s, e) => LoadSettings());
            fileMenu.DropDownItems.Add("Reset to Defaults", null, (s, e) => ResetToDefaults());
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add("Exit", null, (s, e) => ExitApplication());

            // help menu
            var helpMenu = new ToolStripMenuItem("Help");
            helpMenu.DropDownItems.Add("About", null, (s, e) => ShowAbout());
            helpMenu.DropDownItems.Add("Settings Location", null, (s, e) => OpenSettingsLocation());

            _menuStrip.Items.Add(fileMenu);
            _menuStrip.Items.Add(helpMenu);

            this.MainMenuStrip = _menuStrip;
            this.Controls.Add(_menuStrip);
        }

        /// <summary>
        /// sets up the key mappings data grid view
        /// </summary>
        private void SetupMappingsGrid(ref int yPos)
        {
            _mappingsGrid = new DataGridView
            {
                Location = new Point(20, yPos),
                Size = new Size(640, 280),
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // configure columns
            var numpadKeyColumn = new DataGridViewComboBoxColumn
            {
                Name = "NumpadKey",
                HeaderText = "Numpad Key",
                Items = {
                    "NumPad0", "NumPad1", "NumPad2", "NumPad3", "NumPad4",
                    "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9",
                    "Add", "Subtract", "Multiply", "Divide", "Decimal"
                }
            };

            var shortcutColumn = new DataGridViewComboBoxColumn
            {
                Name = "Shortcut",
                HeaderText = "Mapped Shortcut",
                Items = {
                    "CTRL+C", "CTRL+V", "CTRL+X", "CTRL+Z", "CTRL+Y",
                    "CTRL+A", "CTRL+S", "CTRL+F", "CTRL+N", "CTRL+O",
                    "CTRL+P", "CTRL+W", "CTRL+T", "CTRL+SHIFT+S",
                    "ALT+F4", "ALT+TAB", "WIN+D", "WIN+E"
                }
            };

            var descriptionColumn = new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description"
            };

            _mappingsGrid.Columns.Add(numpadKeyColumn);
            _mappingsGrid.Columns.Add(shortcutColumn);
            _mappingsGrid.Columns.Add(descriptionColumn);

            this.Controls.Add(_mappingsGrid);
            yPos += 300;
        }

        /// <summary>
        /// sets up the options/settings section
        /// </summary>
        private void SetupOptionsSection(ref int yPos)
        {
            Label optionsLabel = new Label
            {
                Text = "Options:",
                Location = new Point(20, yPos),
                AutoSize = true,
                Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold)
            };
            this.Controls.Add(optionsLabel);
            yPos += 25;

            _minimizeToTrayCheckbox = new CheckBox
            {
                Text = "Minimize to system tray",
                Location = new Point(20, yPos),
                AutoSize = true
            };
            this.Controls.Add(_minimizeToTrayCheckbox);
            yPos += 25;

            _startWithWindowsCheckbox = new CheckBox
            {
                Text = "Start with Windows",
                Location = new Point(20, yPos),
                AutoSize = true
            };
            this.Controls.Add(_startWithWindowsCheckbox);
            yPos += 35;
        }

        /// <summary>
        /// sets up control buttons
        /// </summary>
        private void SetupButtons(ref int yPos)
        {
            int buttonWidth = 100;
            int buttonHeight = 35;
            int spacing = 10;
            int xPos = 20;

            _startButton = new Button
            {
                Text = "Start Mapping",
                Location = new Point(xPos, yPos),
                Size = new Size(buttonWidth, buttonHeight)
            };
            _startButton.Click += StartButton_Click;
            this.Controls.Add(_startButton);
            xPos += buttonWidth + spacing;

            _stopButton = new Button
            {
                Text = "Stop Mapping",
                Location = new Point(xPos, yPos),
                Size = new Size(buttonWidth, buttonHeight),
                Enabled = false
            };
            _stopButton.Click += StopButton_Click;
            this.Controls.Add(_stopButton);
            xPos += buttonWidth + spacing;

            _saveButton = new Button
            {
                Text = "Save Settings",
                Location = new Point(xPos, yPos),
                Size = new Size(buttonWidth, buttonHeight)
            };
            _saveButton.Click += (s, e) => SaveSettings();
            this.Controls.Add(_saveButton);
            xPos += buttonWidth + spacing;

            _loadButton = new Button
            {
                Text = "Load Settings",
                Location = new Point(xPos, yPos),
                Size = new Size(buttonWidth, buttonHeight)
            };
            _loadButton.Click += (s, e) => LoadSettings();
            this.Controls.Add(_loadButton);
            xPos += buttonWidth + spacing;

            _resetButton = new Button
            {
                Text = "Reset",
                Location = new Point(xPos, yPos),
                Size = new Size(buttonWidth, buttonHeight)
            };
            _resetButton.Click += (s, e) => ResetToDefaults();
            this.Controls.Add(_resetButton);

            yPos += buttonHeight + 20;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// handles start button click - initializes keyboard hook and mapping
        /// </summary>
        private void StartButton_Click(object sender, EventArgs e)
        {
            StartMapping();
        }

        /// <summary>
        /// handles stop button click - removes keyboard hook
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            StopMapping();
        }

        /// <summary>
        /// starts the keyboard mapping
        /// </summary>
        private void StartMapping()
        {
            try
            {
                UpdateShortcutsFromGrid();
                InitializeKeyboardHook();
                _isRunning = true;
                _startButton.Enabled = false;
                _stopButton.Enabled = true;
                _statusLabel.Text = "Status: Running - Monitoring keyboard";
                _statusLabel.ForeColor = Color.Green;

                if (_settings.ShowNotifications)
                {
                    ShowTrayNotification("Keyboard mapping started", "MyNumpad is now active");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting mapping: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// stops the keyboard mapping
        /// </summary>
        private void StopMapping()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
            _isRunning = false;
            _startButton.Enabled = true;
            _stopButton.Enabled = false;
            _statusLabel.Text = "Status: Stopped";
            _statusLabel.ForeColor = Color.Red;

            if (_settings.ShowNotifications)
            {
                ShowTrayNotification("Keyboard mapping stopped", "MyNumpad is now inactive");
            }
        }

        /// <summary>
        /// handles form resize to support minimize to tray
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.WindowState == FormWindowState.Minimized && _minimizeToTrayCheckbox.Checked)
            {
                this.Hide();
                _notifyIcon.Visible = true;
                if (_settings.ShowNotifications)
                {
                    ShowTrayNotification("Minimized to tray", "MyNumpad is still running in the background");
                }
            }
        }

        /// <summary>
        /// handles form closing event
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (_hookID != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(_hookID);
                    _hookID = IntPtr.Zero;
                }

                if (_notifyIcon != null)
                {
                    _notifyIcon.Visible = false;
                    _notifyIcon.Dispose();
                }
            }
            catch (Exception)
            {
                // handle any cleanup exceptions silently
            }
            base.OnFormClosing(e);
        }

        #endregion

        #region Settings Management

        /// <summary>
        /// saves current settings to file
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                SaveSettingsFromUI();
                _settings.Save();
                MessageBox.Show("Settings saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving settings: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// loads settings from file
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                _settings = AppSettings.Load();
                LoadSettingsIntoUI();
                MessageBox.Show("Settings loaded successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading settings: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// resets settings to default values
        /// </summary>
        private void ResetToDefaults()
        {
            var result = MessageBox.Show(
                "Are you sure you want to reset all settings to defaults?",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    AppSettings.ResetToDefaults();
                    _settings = AppSettings.Load();
                    LoadSettingsIntoUI();
                    MessageBox.Show("Settings reset to defaults!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error resetting settings: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// sets or removes application from windows startup
        /// </summary>
        private void SetStartupRegistry(bool enable)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    if (key != null)
                    {
                        if (enable)
                        {
                            string exePath = Application.ExecutablePath;
                            key.SetValue("MyNumpadMapper", exePath);
                        }
                        else
                        {
                            key.DeleteValue("MyNumpadMapper", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating startup settings: " + ex.Message, "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Keyboard Hook and Mapping

        /// <summary>
        /// updates the shortcuts dictionary from the grid data
        /// </summary>
        private void UpdateShortcutsFromGrid()
        {
            _shortcuts.Clear();
            foreach (DataGridViewRow row in _mappingsGrid.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    string numpadKey = row.Cells[0].Value.ToString() ?? string.Empty;
                    string shortcut = row.Cells[1].Value.ToString() ?? string.Empty;

                    if (Enum.TryParse<Keys>(numpadKey, out Keys key))
                    {
                        Action? action = GetShortcutAction(shortcut);
                        if (action != null)
                        {
                            _shortcuts.Add((int)key, action);
                        }
                    }
                }
            }

            UpdateStatusText($"Loaded {_shortcuts.Count} key mappings");
        }

        /// <summary>
        /// gets the action corresponding to a shortcut string
        /// </summary>
        private Action? GetShortcutAction(string shortcut)
        {
            return shortcut.ToUpper() switch
            {
                "CTRL+C" => new Action(SendCopyCommand),
                "CTRL+V" => new Action(SendPasteCommand),
                "CTRL+X" => new Action(SendCutCommand),
                "CTRL+Z" => new Action(SendUndoCommand),
                "CTRL+Y" => new Action(SendRedoCommand),
                "CTRL+A" => new Action(SendSelectAllCommand),
                "CTRL+S" => new Action(SendSaveCommand),
                "CTRL+F" => new Action(SendFindCommand),
                "CTRL+N" => new Action(SendNewCommand),
                "CTRL+O" => new Action(SendOpenCommand),
                "CTRL+P" => new Action(SendPrintCommand),
                "CTRL+W" => new Action(SendCloseCommand),
                "CTRL+T" => new Action(SendNewTabCommand),
                "CTRL+SHIFT+S" => new Action(SendSaveAsCommand),
                _ => null
            };
        }

        /// <summary>
        /// initializes the low-level keyboard hook
        /// </summary>
        private void InitializeKeyboardHook()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        /// <summary>
        /// sets up the keyboard hook
        /// </summary>
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule!)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// keyboard hook callback - intercepts and processes key presses
        /// </summary>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                try
                {
                    KBDLLHOOKSTRUCT hookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(
                        lParam, typeof(KBDLLHOOKSTRUCT))!;
                    int vkCode = (int)hookStruct.vkCode;

                    UpdateStatusText($"Key pressed: {((Keys)vkCode)}");

                    // check if this key has a mapped shortcut
                    if (_shortcuts.ContainsKey(vkCode))
                    {
                        Action shortcutAction = _shortcuts[vkCode];
                        ExecuteShortcut(shortcutAction);
                        // return 1 to block the original key
                        return (IntPtr)1;
                    }
                }
                catch (Exception ex)
                {
                    UpdateStatusText("Error: " + ex.Message);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        #endregion

        #region Shortcut Commands

        /// <summary>
        /// sends ctrl+c (copy) command
        /// </summary>
        private void SendCopyCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.C);
        }

        /// <summary>
        /// sends ctrl+v (paste) command
        /// </summary>
        private void SendPasteCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.V);
        }

        /// <summary>
        /// sends ctrl+x (cut) command
        /// </summary>
        private void SendCutCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.X);
        }

        /// <summary>
        /// sends ctrl+z (undo) command
        /// </summary>
        private void SendUndoCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.Z);
        }

        /// <summary>
        /// sends ctrl+y (redo) command
        /// </summary>
        private void SendRedoCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.Y);
        }

        /// <summary>
        /// sends ctrl+a (select all) command
        /// </summary>
        private void SendSelectAllCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.A);
        }

        /// <summary>
        /// sends ctrl+s (save) command
        /// </summary>
        private void SendSaveCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.S);
        }

        /// <summary>
        /// sends ctrl+f (find) command
        /// </summary>
        private void SendFindCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.F);
        }

        /// <summary>
        /// sends ctrl+n (new) command
        /// </summary>
        private void SendNewCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.N);
        }

        /// <summary>
        /// sends ctrl+o (open) command
        /// </summary>
        private void SendOpenCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.O);
        }

        /// <summary>
        /// sends ctrl+p (print) command
        /// </summary>
        private void SendPrintCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.P);
        }

        /// <summary>
        /// sends ctrl+w (close) command
        /// </summary>
        private void SendCloseCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.W);
        }

        /// <summary>
        /// sends ctrl+t (new tab) command
        /// </summary>
        private void SendNewTabCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.T);
        }

        /// <summary>
        /// sends ctrl+shift+s (save as) command
        /// </summary>
        private void SendSaveAsCommand()
        {
            SendKeyboardShortcut(Keys.ControlKey, Keys.S, Keys.ShiftKey);
        }

        /// <summary>
        /// generic method to send keyboard shortcuts
        /// </summary>
        private void SendKeyboardShortcut(Keys modifierKey, Keys key, Keys? secondModifier = null)
        {
            try
            {
                // press modifier(s)
                keybd_event((byte)modifierKey, 0, 0, UIntPtr.Zero);
                System.Threading.Thread.Sleep(50);

                if (secondModifier.HasValue)
                {
                    keybd_event((byte)secondModifier.Value, 0, 0, UIntPtr.Zero);
                    System.Threading.Thread.Sleep(50);
                }

                // press main key
                keybd_event((byte)key, 0, 0, UIntPtr.Zero);
                System.Threading.Thread.Sleep(50);

                // release main key
                keybd_event((byte)key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                System.Threading.Thread.Sleep(50);

                // release modifier(s)
                if (secondModifier.HasValue)
                {
                    keybd_event((byte)secondModifier.Value, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                    System.Threading.Thread.Sleep(50);
                }

                keybd_event((byte)modifierKey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
            catch (Exception ex)
            {
                UpdateStatusText($"Error sending shortcut: {ex.Message}");
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// updates status label text (thread-safe)
        /// </summary>
        private void UpdateStatusText(string text)
        {
            if (_statusLabel.InvokeRequired)
            {
                try
                {
                    _statusLabel.Invoke(new Action<string>(UpdateStatusText), new object[] { text });
                }
                catch (Exception)
                {
                    // handle invoke exceptions silently
                }
            }
            else
            {
                _statusLabel.Text = text;
            }
        }

        /// <summary>
        /// executes shortcut action (thread-safe)
        /// </summary>
        private void ExecuteShortcut(Action shortcutAction)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(shortcutAction);
                }
                catch (Exception)
                {
                    // handle invoke exceptions silently
                }
            }
            else
            {
                shortcutAction();
            }
        }

        /// <summary>
        /// shows main window from system tray
        /// </summary>
        private void ShowMainWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            _notifyIcon.Visible = false;
        }

        /// <summary>
        /// shows system tray notification
        /// </summary>
        private void ShowTrayNotification(string title, string message)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.BalloonTipTitle = title;
                _notifyIcon.BalloonTipText = message;
                _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                _notifyIcon.ShowBalloonTip(3000);
            }
        }

        /// <summary>
        /// exits the application
        /// </summary>
        private void ExitApplication()
        {
            Application.Exit();
        }

        /// <summary>
        /// shows about dialog
        /// </summary>
        private void ShowAbout()
        {
            MessageBox.Show(
                "MyNumpad Keyboard Mapper v1.0\n\n" +
                "Maps numpad keys to keyboard shortcuts for enhanced productivity.\n\n" +
                "© 2025 MyNumpad Project",
                "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// opens the settings file location in explorer
        /// </summary>
        private void OpenSettingsLocation()
        {
            try
            {
                string settingsPath = AppSettings.GetSettingsPath();
                string directory = System.IO.Path.GetDirectoryName(settingsPath) ?? string.Empty;

                if (System.IO.Directory.Exists(directory))
                {
                    Process.Start("explorer.exe", directory);
                }
                else
                {
                    MessageBox.Show("Settings directory does not exist yet.\nSave settings first to create it.",
                        "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening settings location: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
