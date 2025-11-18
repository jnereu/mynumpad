using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace mynumpad.Config
{
    /// <summary>
    /// represents a single key mapping configuration
    /// </summary>
    public class KeyMapping
    {
        public string NumpadKey { get; set; } = string.Empty;
        public string Shortcut { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// application settings and configuration manager
    /// handles saving and loading key mappings to/from json file
    /// </summary>
    public class AppSettings
    {
        private static readonly string SettingsDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MyNumpad"
        );

        private static readonly string SettingsFilePath = Path.Combine(
            SettingsDirectory,
            "settings.json"
        );

        public List<KeyMapping> KeyMappings { get; set; } = new List<KeyMapping>();
        public bool StartMinimized { get; set; } = false;
        public bool MinimizeToTray { get; set; } = true;
        public bool StartWithWindows { get; set; } = false;
        public bool ShowNotifications { get; set; } = true;
        public string TargetKeyboardId { get; set; } = @"HID\VID_1C4F&PID_0002&REV_0340&MI_00";

        /// <summary>
        /// loads settings from json file or returns default settings if file doesn't exist
        /// </summary>
        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    var settings = JsonConvert.DeserializeObject<AppSettings>(json);
                    return settings ?? GetDefaultSettings();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"error loading settings: {ex.Message}");
            }

            return GetDefaultSettings();
        }

        /// <summary>
        /// saves current settings to json file
        /// </summary>
        public void Save()
        {
            try
            {
                // ensure directory exists
                if (!Directory.Exists(SettingsDirectory))
                {
                    Directory.CreateDirectory(SettingsDirectory);
                }

                // serialize and save
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"error saving settings: {ex.Message}");
                throw new Exception("failed to save settings: " + ex.Message);
            }
        }

        /// <summary>
        /// returns default application settings with pre-configured key mappings
        /// </summary>
        private static AppSettings GetDefaultSettings()
        {
            return new AppSettings
            {
                KeyMappings = new List<KeyMapping>
                {
                    new KeyMapping { NumpadKey = "NumPad1", Shortcut = "CTRL+C", Description = "Copy" },
                    new KeyMapping { NumpadKey = "NumPad2", Shortcut = "CTRL+V", Description = "Paste" },
                    new KeyMapping { NumpadKey = "NumPad3", Shortcut = "CTRL+X", Description = "Cut" },
                    new KeyMapping { NumpadKey = "NumPad4", Shortcut = "CTRL+Z", Description = "Undo" },
                    new KeyMapping { NumpadKey = "NumPad5", Shortcut = "CTRL+S", Description = "Save" },
                    new KeyMapping { NumpadKey = "NumPad6", Shortcut = "CTRL+Y", Description = "Redo" },
                    new KeyMapping { NumpadKey = "NumPad0", Shortcut = "CTRL+A", Description = "Select All" }
                }
            };
        }

        /// <summary>
        /// gets the path where settings are stored
        /// </summary>
        public static string GetSettingsPath()
        {
            return SettingsFilePath;
        }

        /// <summary>
        /// deletes the settings file (resets to defaults)
        /// </summary>
        public static void ResetToDefaults()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    File.Delete(SettingsFilePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("failed to reset settings: " + ex.Message);
            }
        }
    }
}
