# MyNumpad Keyboard Mapper

![.NET](https://img.shields.io/badge/.NET-6.0-blue)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey)
![License](https://img.shields.io/badge/license-MIT-green)

**MyNumpad Keyboard Mapper** é uma aplicação Windows que mapeia teclas do teclado numérico (numpad) para atalhos de teclado personalizados, aumentando a produtividade ao permitir acesso rápido a comandos frequentemente usados.

## 📋 Descrição do Projeto

Este projeto foi desenvolvido para resolver o problema de acesso rápido a atalhos de teclado comuns durante o trabalho. Usando um teclado numérico dedicado ou o numpad do teclado principal, você pode executar comandos como copiar, colar, salvar, desfazer, e muito mais com apenas uma tecla.

A aplicação funciona através de um **keyboard hook de baixo nível** que intercepta teclas específicas do numpad e as transforma em combinações de teclas (como Ctrl+C, Ctrl+V, etc.).

### Características Principais

- ✨ **Mapeamento personalizável** - Configure qualquer tecla do numpad para qualquer atalho
- 💾 **Salvar/Carregar configurações** - Suas configurações são salvas em formato JSON
- 🎯 **Interface intuitiva** - Grid editável para fácil configuração
- 🔔 **Minimizar para system tray** - Fica discretamente na bandeja do sistema
- 🚀 **Iniciar com Windows** - Opção de autostart
- 📊 **Monitoramento em tempo real** - Veja as teclas sendo pressionadas
- 🛠️ **Múltiplos atalhos suportados** - Ctrl, Alt, Win, e combinações

## 🎯 Funcionalidades

### Funcionalidades Atuais

1. **Mapeamento de Teclas**
   - Numpad 0-9
   - Teclas especiais (+, -, *, /, .)
   - Suporte para 15+ atalhos pré-definidos

2. **Atalhos Suportados**
   - `CTRL+C` - Copiar
   - `CTRL+V` - Colar
   - `CTRL+X` - Cortar
   - `CTRL+Z` - Desfazer
   - `CTRL+Y` - Refazer
   - `CTRL+A` - Selecionar Tudo
   - `CTRL+S` - Salvar
   - `CTRL+F` - Encontrar
   - `CTRL+N` - Novo
   - `CTRL+O` - Abrir
   - `CTRL+P` - Imprimir
   - `CTRL+W` - Fechar
   - `CTRL+T` - Nova Aba
   - `CTRL+SHIFT+S` - Salvar Como

3. **Gerenciamento de Configurações**
   - Salvar configurações personalizadas
   - Carregar configurações salvas
   - Resetar para padrões
   - Exportar/Importar via JSON

4. **Interface do Usuário**
   - Grid editável para mapeamentos
   - Indicador de status em tempo real
   - Menu completo (File, Help)
   - System tray integration

5. **Opções**
   - Minimizar para system tray
   - Iniciar com Windows
   - Notificações do sistema

## 🚀 Como Rodar Localmente

### Pré-requisitos

- **Windows 10/11** (64-bit)
- **.NET 6.0 SDK ou superior** - [Download aqui](https://dotnet.microsoft.com/download/dotnet/6.0)
- **Visual Studio 2022** (opcional, mas recomendado) ou **Visual Studio Code**

### Instalação

1. **Clone o repositório**
   ```bash
   git clone https://github.com/seu-usuario/mynumpad.git
   cd mynumpad
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Compile o projeto**
   ```bash
   dotnet build src/mynumpad/mynumpad.csproj --configuration Release
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run --project src/mynumpad/mynumpad.csproj
   ```

   Ou compile e execute o executável diretamente:
   ```bash
   cd src/mynumpad/bin/Release/net6.0-windows
   ./MyNumpad.exe
   ```

### Usando o Visual Studio

1. Abra o arquivo `mynumpad.sln` no Visual Studio 2022
2. Pressione `F5` para compilar e executar em modo debug
3. Ou use `Ctrl+Shift+B` para compilar e depois execute manualmente

## 📖 Como Usar

### Configuração Básica

1. **Inicie a aplicação** - A janela principal será exibida com mapeamentos padrão
2. **Configure seus mapeamentos**:
   - Na grid, selecione a tecla do Numpad (coluna 1)
   - Escolha o atalho desejado (coluna 2)
   - Adicione uma descrição opcional (coluna 3)
3. **Clique em "Start Mapping"** para ativar o hook do teclado
4. **Teste** - Pressione as teclas do numpad para executar os atalhos
5. **Salve as configurações** - Clique em "Save Settings" ou use File > Save Settings

### Opções Avançadas

#### Minimizar para System Tray
- Marque "Minimize to system tray"
- Ao minimizar, o aplicativo ficará na bandeja do sistema
- Clique duas vezes no ícone para restaurar

#### Iniciar com Windows
- Marque "Start with Windows"
- Clique em "Save Settings"
- O aplicativo será executado automaticamente no login

#### Localização dos Arquivos de Configuração
- As configurações são salvas em: `%APPDATA%\MyNumpad\settings.json`
- Acesse via menu: Help > Settings Location

## 🏗️ Estrutura do Projeto

```
mynumpad/
├── src/
│   └── mynumpad/
│       ├── Config/
│       │   └── AppSettings.cs          # Gerenciamento de configurações
│       ├── Properties/
│       ├── Resources/
│       ├── MainForm.cs                 # Formulário principal
│       ├── MainForm.Designer.cs        # Designer do formulário
│       ├── Program.cs                  # Entry point
│       └── mynumpad.csproj             # Arquivo de projeto
├── mynumpad.sln                        # Solution file
└── README.md
```

## 🔧 Configuração JSON

As configurações são salvas em formato JSON:

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

## 💡 Ideias para Expandir

### Funcionalidades Futuras

#### 🌐 Backend e Sincronização
- [ ] **API REST** para sincronizar configurações entre dispositivos
- [ ] **Cloud storage** (Google Drive, Dropbox) para backup automático
- [ ] **Perfis de configuração** - diferentes perfis para diferentes aplicativos
- [ ] **Banco de dados** para histórico de uso e estatísticas

#### 🔐 Autenticação e Multi-usuário
- [ ] Sistema de login/registro
- [ ] Perfis de usuário
- [ ] Compartilhamento de configurações entre usuários
- [ ] Configurações por aplicativo (diferente para VS Code, Chrome, etc.)

#### 🎨 Interface e UX
- [ ] **Temas** - Dark mode, light mode, temas personalizados
- [ ] **Drag-and-drop** na grid para reordenar mapeamentos
- [ ] **Atalhos visuais** - Preview do que cada tecla faz
- [ ] **Hotkey recorder** - Gravar qualquer combinação de teclas
- [ ] **Animações** e feedback visual quando teclas são pressionadas

#### 🚀 Recursos Avançados
- [ ] **Macros** - Sequências de comandos
- [ ] **Scripts personalizados** - Execute scripts quando uma tecla é pressionada
- [ ] **Detecção de aplicativo** - Diferentes mapeamentos para diferentes apps
- [ ] **Perfis automáticos** - Muda perfil baseado no app ativo
- [ ] **Estatísticas de uso** - Quais atalhos você mais usa
- [ ] **Suporte para outros dispositivos** - Mouses programáveis, joysticks
- [ ] **Layers** - Múltiplas camadas de mapeamentos (como vim)

#### 📱 Multiplataforma
- [ ] **Versão Linux** usando X11 ou Wayland
- [ ] **Versão MacOS** usando CGEvent
- [ ] **App mobile** para configuração remota (iOS/Android)
- [ ] **Web app** para gerenciar configurações

#### 🔌 Integrações
- [ ] **Discord/Slack integration** - Executar comandos via bots
- [ ] **IFTTT/Zapier** - Automações
- [ ] **Smart Home** - Controle dispositivos IoT
- [ ] **API pública** - Permitir outros apps se integrarem

#### 🛡️ Segurança e Privacidade
- [ ] **Criptografia** de configurações salvas
- [ ] **Whitelist de aplicativos** - Só funciona em apps específicos
- [ ] **Blacklist de aplicativos** - Desativa em apps sensíveis (bancos, etc.)
- [ ] **Log de auditoria** - Registro de todas as teclas mapeadas

#### 📊 Analytics e Insights
- [ ] Dashboard de estatísticas de uso
- [ ] Gráficos de produtividade
- [ ] Sugestões de mapeamentos baseadas no uso
- [ ] Exportar dados de uso para análise

#### 🎮 Gaming e Produtividade
- [ ] **Perfil para games** - Macros complexos para MMOs
- [ ] **Streaming integration** - Controles para OBS, Streamlabs
- [ ] **Edição de vídeo** - Perfis para Premiere, DaVinci Resolve
- [ ] **CAD/Design** - Perfis para AutoCAD, Photoshop, etc.

### Melhorias Técnicas

#### Performance
- [ ] Otimização do keyboard hook
- [ ] Cache de configurações
- [ ] Lazy loading de recursos

#### Código
- [ ] Unit tests
- [ ] Integration tests
- [ ] CI/CD pipeline (GitHub Actions)
- [ ] Code coverage
- [ ] Documentação XML completa
- [ ] Logging estruturado (Serilog, NLog)

#### Distribuição
- [ ] Installer (WiX, Inno Setup)
- [ ] Auto-update (Squirrel.Windows)
- [ ] Microsoft Store
- [ ] Portable version (single exe)
- [ ] MSI package para enterprise

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 👨‍💻 Autor

**MyNumpad Project**

## 🙏 Agradecimentos

- Comunidade .NET
- Windows API documentation
- Todos os contribuidores

## 📞 Suporte

Se você encontrar algum problema ou tiver sugestões:
- Abra uma [issue](https://github.com/seu-usuario/mynumpad/issues)
- Envie um email: support@mynumpad.com

---

⌨️ Feito com ❤️ para aumentar sua produtividade!
