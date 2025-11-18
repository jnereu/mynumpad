# Guia de Uso

Este guia detalha como usar o MyNumpad Keyboard Mapper no dia a dia.

## Índice

1. [Início Rápido](#início-rápido)
2. [Interface do Usuário](#interface-do-usuário)
3. [Configuração de Mapeamentos](#configuração-de-mapeamentos)
4. [Atalhos Disponíveis](#atalhos-disponíveis)
5. [Casos de Uso](#casos-de-uso)
6. [Dicas e Truques](#dicas-e-truques)

## Início Rápido

### Fluxo Básico de Uso

1. **Abra o MyNumpad**
2. **Revise os mapeamentos padrão** na grid
3. **Clique em "Start Mapping"**
4. **Use as teclas do numpad** para executar atalhos
5. **Personalize conforme necessário**

### Exemplo Prático

Imagine que você está editando um documento:

1. Selecione um texto com o mouse
2. Pressione **NumPad1** → Copia o texto (Ctrl+C)
3. Mova o cursor para outro lugar
4. Pressione **NumPad2** → Cola o texto (Ctrl+V)

## Interface do Usuário

### Janela Principal

```
┌─────────────────────────────────────────────────────┐
│ File    Help                                        │
├─────────────────────────────────────────────────────┤
│ Target Keyboard: HID\VID_1C4F&PID_0002...          │
│                                                     │
│ ┌─────────────────────────────────────────────┐   │
│ │ Numpad Key │ Mapped Shortcut │ Description  │   │
│ ├────────────┼─────────────────┼──────────────┤   │
│ │ NumPad1    │ CTRL+C          │ Copy         │   │
│ │ NumPad2    │ CTRL+V          │ Paste        │   │
│ │ ...        │ ...             │ ...          │   │
│ └─────────────────────────────────────────────┘   │
│                                                     │
│ Options:                                            │
│ ☑ Minimize to system tray                          │
│ ☐ Start with Windows                               │
│                                                     │
│ [Start] [Stop] [Save] [Load] [Reset]               │
│                                                     │
│ Status: Running - Monitoring keyboard              │
└─────────────────────────────────────────────────────┘
```

### Elementos da Interface

#### 1. Menu Bar
- **File**: Salvar, Carregar, Resetar, Sair
- **Help**: Sobre, Localização das Configurações

#### 2. Grid de Mapeamentos
- **Numpad Key**: Tecla do numpad a ser mapeada
- **Mapped Shortcut**: Atalho que será executado
- **Description**: Descrição do que faz (opcional)

#### 3. Opções
- **Minimize to system tray**: Minimiza para a bandeja
- **Start with Windows**: Inicia automaticamente

#### 4. Botões de Controle
- **Start Mapping**: Inicia o monitoramento de teclas
- **Stop Mapping**: Para o monitoramento
- **Save Settings**: Salva configurações atuais
- **Load Settings**: Carrega configurações salvas
- **Reset**: Reseta para configurações padrão

#### 5. Status
- Mostra o estado atual (Running/Stopped)
- Exibe a última tecla pressionada

## Configuração de Mapeamentos

### Adicionar Novo Mapeamento

1. **Clique na última linha vazia** da grid
2. **Selecione a tecla do Numpad** no dropdown (coluna 1)
3. **Selecione o atalho** no dropdown (coluna 2)
4. **Adicione uma descrição** (opcional, coluna 3)
5. **Clique em "Save Settings"**

### Editar Mapeamento Existente

1. **Clique na célula** que deseja editar
2. **Selecione o novo valor** no dropdown
3. **Clique em "Save Settings"**

### Remover Mapeamento

1. **Clique na linha** que deseja remover
2. **Pressione Delete** no teclado
3. **Clique em "Save Settings"**

### Exemplo de Configuração Personalizada

Para edição de vídeo:

| Numpad Key | Shortcut | Description |
|------------|----------|-------------|
| NumPad1 | CTRL+C | Copy clip |
| NumPad2 | CTRL+V | Paste clip |
| NumPad3 | CTRL+X | Cut clip |
| NumPad4 | CTRL+Z | Undo |
| NumPad5 | SPACE | Play/Pause |
| NumPad7 | CTRL+S | Save project |
| NumPad8 | CTRL+N | New sequence |

## Atalhos Disponíveis

### Atalhos de Edição

| Atalho | Função | Uso Comum |
|--------|--------|-----------|
| CTRL+C | Copiar | Copiar texto/arquivos |
| CTRL+V | Colar | Colar conteúdo |
| CTRL+X | Cortar | Cortar texto/arquivos |
| CTRL+Z | Desfazer | Desfazer última ação |
| CTRL+Y | Refazer | Refazer ação desfeita |
| CTRL+A | Selecionar Tudo | Selecionar todo conteúdo |

### Atalhos de Arquivo

| Atalho | Função | Uso Comum |
|--------|--------|-----------|
| CTRL+S | Salvar | Salvar documento |
| CTRL+SHIFT+S | Salvar Como | Salvar com novo nome |
| CTRL+N | Novo | Novo documento |
| CTRL+O | Abrir | Abrir arquivo |
| CTRL+P | Imprimir | Imprimir documento |
| CTRL+W | Fechar | Fechar aba/janela |

### Atalhos de Navegação

| Atalho | Função | Uso Comum |
|--------|--------|-----------|
| CTRL+F | Encontrar | Buscar texto |
| CTRL+T | Nova Aba | Abrir nova aba (navegador) |

## Casos de Uso

### 1. Programação

**Configuração sugerida para desenvolvedores:**

```
NumPad1 → CTRL+C (Copiar código)
NumPad2 → CTRL+V (Colar código)
NumPad3 → CTRL+X (Cortar código)
NumPad4 → CTRL+Z (Desfazer)
NumPad5 → CTRL+S (Salvar arquivo)
NumPad6 → CTRL+F (Buscar no arquivo)
NumPad7 → CTRL+SHIFT+S (Salvar tudo)
```

**Benefícios:**
- Copiar/colar com uma mão
- Salvar rapidamente durante digitação
- Desfazer erros instantaneamente

### 2. Edição de Texto

**Configuração sugerida para escritores:**

```
NumPad1 → CTRL+C (Copiar)
NumPad2 → CTRL+V (Colar)
NumPad4 → CTRL+Z (Desfazer)
NumPad5 → CTRL+S (Salvar)
NumPad6 → CTRL+Y (Refazer)
NumPad0 → CTRL+A (Selecionar tudo)
NumPad7 → CTRL+F (Buscar)
```

### 3. Design Gráfico

**Configuração sugerida para designers:**

```
NumPad1 → CTRL+C (Copiar camada)
NumPad2 → CTRL+V (Colar camada)
NumPad3 → CTRL+X (Cortar)
NumPad4 → CTRL+Z (Desfazer)
NumPad5 → CTRL+S (Salvar)
NumPad6 → CTRL+Y (Refazer)
```

### 4. Navegação Web

**Configuração sugerida para navegação:**

```
NumPad1 → CTRL+C (Copiar link/texto)
NumPad2 → CTRL+V (Colar na busca)
NumPad5 → CTRL+S (Salvar página)
NumPad7 → CTRL+T (Nova aba)
NumPad8 → CTRL+W (Fechar aba)
NumPad9 → CTRL+F (Buscar na página)
```

## Dicas e Truques

### Maximize sua Produtividade

#### 1. Organize por Frequência
Coloque os atalhos mais usados nas teclas mais acessíveis:
- **NumPad5** (centro): Ação mais comum (ex: Salvar)
- **NumPad4, 6, 2, 8** (cruz): Ações frequentes
- **NumPad1, 3, 7, 9** (cantos): Ações menos frequentes

#### 2. Agrupe por Função
Organize atalhos relacionados próximos:
- **1-2-3**: Copiar, Colar, Cortar (edição)
- **4-5-6**: Desfazer, Salvar, Refazer (controle)
- **7-8-9**: Buscar, Novo, Abrir (arquivo)

#### 3. Use Descrições Claras
Adicione descrições que você entenda rapidamente:
- ✅ "Copy selected text"
- ✅ "Save current file"
- ❌ "C+C"
- ❌ "s"

#### 4. Crie Perfis para Diferentes Aplicações
Embora não haja suporte nativo para perfis múltiplos na v1.0, você pode:
- Salvar configurações diferentes em arquivos `.json` separados
- Renomear: `settings-coding.json`, `settings-design.json`
- Trocar manualmente conforme necessário

#### 5. Minimize para Tray
- Marque "Minimize to system tray" para manter o app rodando sem ocupar a barra de tarefas
- Acesse rapidamente clicando duas vezes no ícone

#### 6. Autostart para Conveniência
- Marque "Start with Windows" se usar diariamente
- O app estará sempre pronto quando você fizer login

### Atalhos do Próprio MyNumpad

| Ação | Como Fazer |
|------|------------|
| Salvar configurações | File > Save ou botão "Save Settings" |
| Carregar configurações | File > Load ou botão "Load Settings" |
| Resetar configurações | File > Reset to Defaults ou botão "Reset" |
| Ver localização das configs | Help > Settings Location |
| Minimizar para tray | Minimizar a janela (se opção ativada) |
| Restaurar da tray | Duplo clique no ícone da bandeja |

### Solução Rápida de Problemas

**Tecla não responde?**
1. Verifique se "Start Mapping" está ativo
2. Confirme que a tecla está mapeada na grid
3. Teste a tecla em outro app (Notepad)

**Atalho não funciona em app específico?**
- Alguns apps (especialmente games) bloqueiam hooks externos
- Execute MyNumpad como Administrador

**Múltiplas teclas executam o mesmo atalho?**
- Verifique duplicatas na grid
- Remova mapeamentos duplicados

## Melhores Práticas

### Para Iniciantes

1. **Comece com os padrões**: Use os mapeamentos padrão por alguns dias
2. **Identifique padrões**: Note quais atalhos você usa mais
3. **Personalize gradualmente**: Ajuste um ou dois mapeamentos por vez
4. **Salve frequentemente**: Clique em "Save Settings" após cada mudança

### Para Usuários Avançados

1. **Experimente combinações**: Teste diferentes layouts
2. **Backup regular**: Copie `settings.json` periodicamente
3. **Documente seu layout**: Use descrições detalhadas
4. **Compartilhe configurações**: Exporte seu `settings.json` para colegas

## Próximos Passos

- Explore o [README.md](../README.md) para ver roadmap de features futuras
- Contribua com sugestões no [GitHub Issues](https://github.com/seu-usuario/mynumpad/issues)
- Compartilhe suas configurações favoritas com a comunidade

---

Última atualização: 2025-01-18
