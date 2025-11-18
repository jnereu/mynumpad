# MyNumpad Keyboard Mapper

![.NET](https://img.shields.io/badge/.NET-6.0-blue)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey)
![License](https://img.shields.io/badge/license-MIT-green)

**MyNumpad Keyboard Mapper** is a Windows application that maps numpad keys to custom keyboard shortcuts, boosting productivity by allowing quick access to frequently used commands.

## 📋 Project Description

This project was developed to solve the problem of quick access to common keyboard shortcuts during work. Using a dedicated numeric keypad or the main keyboard's numpad, you can execute commands like copy, paste, save, undo, and much more with just one key.

The application works through a **low-level keyboard hook** that intercepts specific numpad keys and transforms them into key combinations (like Ctrl+C, Ctrl+V, etc.).
![MyNumPad](https://raw.githubusercontent.com/jnereu/mynumpad/refs/heads/jnereu-patch-1/docs/mynumpad.png)
### Key Features

- ✨ **Customizable mapping** - Configure any numpad key to any shortcut
- 💾 **Save/Load settings** - Your settings are saved in JSON format
- 🎯 **Intuitive interface** - Editable grid for easy configuration
- 🔔 **Minimize to system tray** - Stays discreetly in the system tray
- 🚀 **Start with Windows** - Autostart option
- 📊 **Real-time monitoring** - See which keys are being pressed
- 🛠️ **Multiple shortcuts supported** - Ctrl, Alt, Win, and combinations

## 🎯 Features

### Current Features

1. **Key Mapping**
   - Numpad 0-9
   - Special keys (+, -, *, /, .)
   - Support for 15+ pre-defined shortcuts

2. **Supported Shortcuts**
   - `CTRL+C` - Copy
   - `CTRL+V` - Paste
   - `CTRL+X` - Cut
   - `CTRL+Z` - Undo
   - `CTRL+Y` - Redo
   - `CTRL+A` - Select All
   - `CTRL+S` - Save
   - `CTRL+F` - Find
   - `CTRL+N` - New
   - `CTRL+O` - Open
   - `CTRL+P` - Print
   - `CTRL+W` - Close
   - `CTRL+T` - New Tab
   - `CTRL+SHIFT+S` - Save As

3. **Settings Management**
   - Save custom settings
   - Load saved settings
   - Reset to defaults
   - Export/Import via JSON

4. **User Interface**
   - Editable grid for mappings
   - Real-time status indicator
   - Complete menu (File, Help)
   - System tray integration

5. **Options**
   - Minimize to system tray
   - Start with Windows
   - System notifications

## 🚀 How to Run Locally

### Prerequisites

- **Windows 10/11** (64-bit)
- **.NET 6.0 SDK or higher** - [Download here](https://dotnet.microsoft.com/download/dotnet/6.0)
- **Visual Studio 2022** (optional, but recommended) or **Visual Studio Code**

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/mynumpad.git
   cd mynumpad
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build src/mynumpad/mynumpad.csproj --configuration Release
   ```

4. **Run the application**
   ```bash
   dotnet run --project src/mynumpad/mynumpad.csproj
   ```

   Or build and run the executable directly:
   ```bash
   cd src/mynumpad/bin/Release/net6.0-windows
   ./MyNumpad.exe
   ```

### Using Visual Studio

1. Open the `mynumpad.sln` file in Visual Studio 2022
2. Press `F5` to build and run in debug mode
3. Or use `Ctrl+Shift+B` to build and then run manually

## 📖 How to Use

### Basic Configuration

1. **Start the application** - The main window will be displayed with default mappings
2. **Configure your mappings**:
   - In the grid, select the Numpad key (column 1)
   - Choose the desired shortcut (column 2)
   - Add an optional description (column 3)
3. **Click "Start Mapping"** to activate the keyboard hook
4. **Test** - Press numpad keys to execute shortcuts
5. **Save settings** - Click "Save Settings" or use File > Save Settings

### Advanced Options

#### Minimize to System Tray
- Check "Minimize to system tray"
- When minimized, the application will stay in the system tray
- Double-click the icon to restore

#### Start with Windows
- Check "Start with Windows"
- Click "Save Settings"
- The application will run automatically on login

#### Configuration File Location
- Settings are saved in: `%APPDATA%\MyNumpad\settings.json`
- Access via menu: Help > Settings Location

## 🏗️ Project Structure

```
mynumpad/
├── src/
│   └── mynumpad/
│       ├── Config/
│       │   └── AppSettings.cs          # Settings management
│       ├── Properties/
│       ├── Resources/
│       ├── MainForm.cs                 # Main form
│       ├── MainForm.Designer.cs        # Form designer
│       ├── Program.cs                  # Entry point
│       └── mynumpad.csproj             # Project file
├── mynumpad.sln                        # Solution file
└── README.md
```

## 🔧 JSON Configuration

Settings are saved in JSON format:

```json
{
  "KeyMappings": [
    {
      "NumpadKey": "NumPad1",
      "Shortcut": "CTRL+C",
      "Description": "Copy"
    },
    {
      "NumpadKey": "NumPad2",
      "Shortcut": "CTRL+V",
      "Description": "Paste"
    }
  ],
  "StartMinimized": false,
  "MinimizeToTray": true,
  "StartWithWindows": false,
  "ShowNotifications": true,
  "TargetKeyboardId": "HID\\VID_1C4F&PID_0002&REV_0340&MI_00"
}
```

## 💡 Ideas for Expansion

### Future Features

#### 🌐 Backend and Synchronization
- [ ] **REST API** to sync settings across devices
- [ ] **Cloud storage** (Google Drive, Dropbox) for automatic backup
- [ ] **Configuration profiles** - different profiles for different applications
- [ ] **Database** for usage history and statistics

#### 🔐 Authentication and Multi-user
- [ ] Login/registration system
- [ ] User profiles
- [ ] Share configurations between users
- [ ] Per-application settings (different for VS Code, Chrome, etc.)

#### 🎨 Interface and UX
- [ ] **Themes** - Dark mode, light mode, custom themes
- [ ] **Drag-and-drop** in grid to reorder mappings
- [ ] **Visual shortcuts** - Preview of what each key does
- [ ] **Hotkey recorder** - Record any key combination
- [ ] **Animations** and visual feedback when keys are pressed

#### 🚀 Advanced Features
- [ ] **Macros** - Command sequences
- [ ] **Custom scripts** - Execute scripts when a key is pressed
- [ ] **Application detection** - Different mappings for different apps
- [ ] **Automatic profiles** - Change profile based on active app
- [ ] **Usage statistics** - Which shortcuts you use most
- [ ] **Support for other devices** - Programmable mice, joysticks
- [ ] **Layers** - Multiple mapping layers (like vim)

#### 📱 Cross-platform
- [ ] **Linux version** using X11 or Wayland
- [ ] **MacOS version** using CGEvent
- [ ] **Mobile app** for remote configuration (iOS/Android)
- [ ] **Web app** to manage settings

#### 🔌 Integrations
- [ ] **Discord/Slack integration** - Execute commands via bots
- [ ] **IFTTT/Zapier** - Automations
- [ ] **Smart Home** - Control IoT devices
- [ ] **Public API** - Allow other apps to integrate

#### 🛡️ Security and Privacy
- [ ] **Encryption** of saved settings
- [ ] **Application whitelist** - Only works with specific apps
- [ ] **Application blacklist** - Disable in sensitive apps (banking, etc.)
- [ ] **Audit log** - Record all mapped keys

#### 📊 Analytics and Insights
- [ ] Usage statistics dashboard
- [ ] Productivity graphs
- [ ] Mapping suggestions based on usage
- [ ] Export usage data for analysis

#### 🎮 Gaming and Productivity
- [ ] **Gaming profiles** - Complex macros for MMOs
- [ ] **Streaming integration** - Controls for OBS, Streamlabs
- [ ] **Video editing** - Profiles for Premiere, DaVinci Resolve
- [ ] **CAD/Design** - Profiles for AutoCAD, Photoshop, etc.

### Technical Improvements

#### Performance
- [ ] Keyboard hook optimization
- [ ] Settings cache
- [ ] Lazy loading of resources

#### Code
- [ ] Unit tests
- [ ] Integration tests
- [ ] CI/CD pipeline (GitHub Actions)
- [ ] Code coverage
- [ ] Complete XML documentation
- [ ] Structured logging (Serilog, NLog)

#### Distribution
- [ ] Installer (WiX, Inno Setup)
- [ ] Auto-update (Squirrel.Windows)
- [ ] Microsoft Store
- [ ] Portable version (single exe)
- [ ] MSI package for enterprise

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the project
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## 👨‍💻 Author

**MyNumpad Project**

## 🙏 Acknowledgments

- .NET Community
- Windows API documentation
- All contributors

## 📞 Support

If you encounter any issues or have suggestions:
- Open an [issue](https://github.com/your-username/mynumpad/issues)
- Send an email: support@mynumpad.com

---

⌨️ Made with ❤️ to boost your productivity!
