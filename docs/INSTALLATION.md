# Guia de Instalação

Este guia detalha os passos para instalar e configurar o MyNumpad Keyboard Mapper.

## Requisitos do Sistema

### Hardware
- **Processador**: x86/x64 compatível
- **Memória RAM**: 512 MB mínimo (1 GB recomendado)
- **Espaço em Disco**: 50 MB
- **Teclado Numérico**: Numpad físico ou integrado

### Software
- **Sistema Operacional**: Windows 10 (64-bit) ou superior
- **Framework**: .NET 6.0 Runtime ou SDK
  - [Download .NET 6.0 Runtime](https://dotnet.microsoft.com/download/dotnet/6.0)
  - [Download .NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) (para desenvolvimento)

## Métodos de Instalação

### Opção 1: Instalação via Executável (Recomendado para Usuários)

1. **Baixe a última versão**
   - Acesse a página de [Releases](https://github.com/seu-usuario/mynumpad/releases)
   - Baixe o arquivo `MyNumpad-v1.0.0-win-x64.zip`

2. **Extraia os arquivos**
   - Descompacte o arquivo ZIP para uma pasta de sua escolha
   - Exemplo: `C:\Program Files\MyNumpad\`

3. **Execute o aplicativo**
   - Localize o arquivo `MyNumpad.exe`
   - Clique duas vezes para executar
   - (Opcional) Crie um atalho na área de trabalho

### Opção 2: Compilação a partir do Código Fonte

#### Pré-requisitos
- Git instalado
- .NET 6.0 SDK ou superior
- Visual Studio 2022 (opcional)

#### Passos

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

   Para modo Debug:
   ```bash
   dotnet build src/mynumpad/mynumpad.csproj --configuration Debug
   ```

   Para modo Release:
   ```bash
   dotnet build src/mynumpad/mynumpad.csproj --configuration Release
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run --project src/mynumpad/mynumpad.csproj
   ```

   Ou navegue até o executável:
   ```bash
   cd src/mynumpad/bin/Release/net6.0-windows
   ./MyNumpad.exe
   ```

### Opção 3: Publicação Standalone

Para criar um executável standalone (sem necessidade de .NET Runtime instalado):

```bash
dotnet publish src/mynumpad/mynumpad.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

O executável será criado em: `src/mynumpad/bin/Release/net6.0-windows/win-x64/publish/`

## Configuração Inicial

### Primeira Execução

1. **Execute MyNumpad.exe**
   - A janela principal será exibida
   - Configurações padrão serão carregadas

2. **Configure seus mapeamentos**
   - A grid mostra os mapeamentos padrão (NumPad1 = Ctrl+C, etc.)
   - Edite conforme sua preferência

3. **Salve as configurações**
   - Clique em "Save Settings" ou use File > Save Settings
   - As configurações serão salvas em: `%APPDATA%\MyNumpad\settings.json`

4. **Inicie o mapeamento**
   - Clique em "Start Mapping"
   - O status mudará para "Running"
   - Suas teclas do numpad agora executam os atalhos configurados

### Configurações Opcionais

#### Iniciar com Windows
1. Marque a opção "Start with Windows"
2. Clique em "Save Settings"
3. O aplicativo será adicionado ao registro do Windows para autostart

#### Minimizar para System Tray
1. Marque "Minimize to system tray"
2. Ao minimizar a janela, o app ficará na bandeja do sistema
3. Clique duas vezes no ícone para restaurar

## Solução de Problemas

### O aplicativo não inicia

**Problema**: Erro ao executar MyNumpad.exe

**Soluções**:
1. Verifique se o .NET 6.0 Runtime está instalado:
   ```bash
   dotnet --version
   ```
   Se não estiver instalado, baixe de: https://dotnet.microsoft.com/download/dotnet/6.0

2. Execute como Administrador:
   - Clique com botão direito em MyNumpad.exe
   - Selecione "Executar como administrador"

3. Verifique o Windows Defender/Antivírus:
   - Alguns antivírus podem bloquear keyboard hooks
   - Adicione MyNumpad.exe às exceções

### As teclas não são mapeadas

**Problema**: Pressionar teclas do numpad não executa atalhos

**Soluções**:
1. Certifique-se de que "Start Mapping" foi clicado
2. Verifique se o NumLock está ativado
3. Confirme que os mapeamentos estão configurados corretamente
4. Verifique o status: deve mostrar "Status: Running"
5. Reinicie o aplicativo como Administrador

### Configurações não são salvas

**Problema**: Ao fechar e reabrir, configurações são perdidas

**Soluções**:
1. Clique em "Save Settings" antes de fechar
2. Verifique permissões da pasta: `%APPDATA%\MyNumpad\`
3. Execute como Administrador se necessário

### Conflito com outras aplicações

**Problema**: Alguns programas não respondem aos atalhos

**Soluções**:
1. Alguns aplicativos (especialmente games) podem bloquear hooks externos
2. Execute MyNumpad como Administrador
3. Teste em aplicativos diferentes (Notepad, Chrome, VS Code)

## Desinstalação

### Remover o Aplicativo

1. **Pare o aplicativo**
   - Clique em "Stop Mapping"
   - Feche a janela

2. **Remova do autostart** (se configurado)
   - Desmarque "Start with Windows"
   - Clique em "Save Settings"
   - Ou manualmente via:
     - `Win + R` > `shell:startup`
     - Delete o atalho "MyNumpad"

3. **Delete os arquivos**
   - Delete a pasta onde instalou o MyNumpad
   - Exemplo: `C:\Program Files\MyNumpad\`

4. **Remova as configurações** (opcional)
   - Delete a pasta: `%APPDATA%\MyNumpad\`
   - Ou use File > Reset to Defaults antes de deletar

### Limpeza do Registro (se necessário)

Se usou "Start with Windows":
1. Abra o Editor de Registro (`regedit`)
2. Navegue para: `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run`
3. Delete a entrada "MyNumpadMapper"

## Atualizações

### Como Atualizar

1. **Baixe a nova versão**
   - Acesse a página de Releases
   - Baixe a versão mais recente

2. **Backup das configurações** (opcional)
   - Copie: `%APPDATA%\MyNumpad\settings.json`

3. **Substitua os arquivos**
   - Feche o MyNumpad se estiver rodando
   - Substitua os arquivos antigos pelos novos

4. **Execute a nova versão**
   - Suas configurações serão mantidas automaticamente

## Suporte

Se encontrar problemas não listados aqui:
- Consulte o [README.md](../README.md)
- Abra uma [Issue no GitHub](https://github.com/seu-usuario/mynumpad/issues)
- Verifique [Issues existentes](https://github.com/seu-usuario/mynumpad/issues?q=is%3Aissue)

## Recursos Adicionais

- [README.md](../README.md) - Visão geral do projeto
- [CONTRIBUTING.md](../CONTRIBUTING.md) - Guia para contribuidores
- [CHANGELOG.md](../CHANGELOG.md) - Histórico de versões
- [GitHub Issues](https://github.com/seu-usuario/mynumpad/issues) - Reportar bugs

---

Última atualização: 2025-01-18
