# Guia de Contribuição

Obrigado por considerar contribuir para o MyNumpad Keyboard Mapper!

## Como Contribuir

### Reportando Bugs

Se você encontrou um bug, por favor:

1. Verifique se o bug já não foi reportado nas [Issues](https://github.com/seu-usuario/mynumpad/issues)
2. Crie uma nova issue com:
   - Título descritivo
   - Descrição detalhada do problema
   - Passos para reproduzir
   - Comportamento esperado vs. atual
   - Screenshots (se aplicável)
   - Informações do sistema (Windows version, .NET version)

### Sugerindo Melhorias

Para sugerir novas funcionalidades:

1. Verifique se a sugestão já não existe
2. Crie uma issue com a tag "enhancement"
3. Descreva claramente a funcionalidade e o caso de uso

### Pull Requests

1. **Fork o repositório**
   ```bash
   git clone https://github.com/seu-usuario/mynumpad.git
   ```

2. **Crie uma branch**
   ```bash
   git checkout -b feature/minha-feature
   ```

3. **Faça suas alterações**
   - Escreva código limpo e bem documentado
   - Adicione comentários em lowercase (conforme padrão do projeto)
   - Siga as convenções de código C#

4. **Teste suas alterações**
   - Compile o projeto
   - Teste todas as funcionalidades afetadas
   - Verifique se não há regressões

5. **Commit suas mudanças**
   ```bash
   git commit -m "feat: adiciona suporte para macros personalizados"
   ```

   Formato de commit messages:
   - `feat:` - nova funcionalidade
   - `fix:` - correção de bug
   - `docs:` - mudanças na documentação
   - `style:` - formatação, sem mudanças de código
   - `refactor:` - refatoração de código
   - `test:` - adição de testes
   - `chore:` - mudanças em ferramentas, configs

6. **Push para o GitHub**
   ```bash
   git push origin feature/minha-feature
   ```

7. **Abra um Pull Request**
   - Descreva suas mudanças
   - Referencie issues relacionadas
   - Adicione screenshots/gifs se relevante

## Padrões de Código

### C# Conventions

- Use PascalCase para classes, métodos e propriedades
- Use camelCase para variáveis locais e parâmetros
- Use `_camelCase` para campos privados
- **Comentários em lowercase** (padrão específico deste projeto)
- Documente métodos públicos com XML comments

### Exemplo

```csharp
/// <summary>
/// sends keyboard shortcut to the active window
/// </summary>
/// <param name="modifierKey">modifier key like ctrl, alt</param>
/// <param name="key">main key to press</param>
private void SendKeyboardShortcut(Keys modifierKey, Keys key)
{
    // implementation here
}
```

## Estrutura de Código

- Organize código em regiões lógicas (#region)
- Mantenha métodos pequenos e focados
- Evite duplicação de código
- Use LINQ quando apropriado
- Trate exceções adequadamente

## Testes

Ao adicionar novas funcionalidades:

1. Teste manualmente todas as funcionalidades
2. Teste em diferentes versões do Windows (se possível)
3. Verifique compatibilidade com diferentes teclados
4. Teste cenários de erro

## Documentação

- Atualize o README.md se necessário
- Adicione comentários XML para APIs públicas
- Documente configurações complexas
- Atualize CHANGELOG.md

## Processo de Review

1. Pull requests serão revisados por mantenedores
2. Podem ser solicitadas mudanças
3. Discussões são bem-vindas
4. Após aprovação, o PR será merged

## Código de Conduta

- Seja respeitoso e profissional
- Aceite críticas construtivas
- Foque no que é melhor para o projeto
- Seja paciente com outros contribuidores

## Dúvidas?

Se tiver dúvidas:
- Abra uma issue com a tag "question"
- Entre em contato com os mantenedores
- Consulte a documentação existente

Obrigado por contribuir! 🎉
