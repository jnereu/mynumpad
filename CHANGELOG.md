# Changelog

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

O formato é baseado em [Keep a Changelog](https://keepachangelog.com/pt-BR/1.0.0/),
e este projeto adere ao [Semantic Versioning](https://semver.org/lang/pt-BR/).

## [1.0.0] - 2025-01-18

### Adicionado
- Mapeamento inicial de teclas do numpad para atalhos de teclado
- Suporte para 15+ atalhos pré-configurados (Ctrl+C, Ctrl+V, etc.)
- Interface gráfica com DataGridView editável
- Sistema de salvamento/carregamento de configurações em JSON
- Minimizar para system tray
- Opção de iniciar com Windows
- Notificações do sistema
- Menu completo (File, Help)
- Hooks de teclado de baixo nível para interceptar teclas
- Prevenção de múltiplas instâncias da aplicação
- Indicador de status em tempo real
- Comentários em lowercase em todo o código
- Documentação completa no README.md

### Funcionalidades
- **Teclas suportadas**: NumPad0-9, Add, Subtract, Multiply, Divide, Decimal
- **Atalhos suportados**:
  - CTRL+C (Copy)
  - CTRL+V (Paste)
  - CTRL+X (Cut)
  - CTRL+Z (Undo)
  - CTRL+Y (Redo)
  - CTRL+A (Select All)
  - CTRL+S (Save)
  - CTRL+F (Find)
  - CTRL+N (New)
  - CTRL+O (Open)
  - CTRL+P (Print)
  - CTRL+W (Close)
  - CTRL+T (New Tab)
  - CTRL+SHIFT+S (Save As)

### Técnico
- .NET 6.0 Windows Forms
- Newtonsoft.Json para serialização
- System.Management para detecção de hardware
- Win32 API para keyboard hooks
- Registry integration para autostart

## [Unreleased]

### Planejado
- Suporte para macros personalizados
- Detecção automática de aplicativo ativo
- Múltiplos perfis de configuração
- Tema dark mode
- Hotkey recorder
- Estatísticas de uso
- Cloud sync
- Versão portable

---

[1.0.0]: https://github.com/seu-usuario/mynumpad/releases/tag/v1.0.0
