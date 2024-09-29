# CraftCore
 
CraftCore é uma implementação de servidor de Minecraft desenvolvida 100% em C#, usando uma biblioteca própria para lidar com o protocolo de comunicação do jogo. O projeto suporta a versão estável mais recente do Minecraft, com possibilidade de funcionar em outras versões.

## Funcionalidades

- Suporte ao protocolo da versão estável mais recente do Minecraft.
- Status do servidor, permitindo aparecer na lista de servidores do cliente.
- Suporte a boa parte do processo de login (usuário ainda não pode entrar no mundo).
- Fácil modificação para criação de minigames e outras aplicações.
- Código 100% aberto e modular, licenciado sob a licença MIT.

## Requisitos

O CraftCore possui algumas dependências externas para funcionar:

- .NET 8.0 ou superior
- Newtonsoft.Json
- YamlDotNet
- fNbt

## Como começar

1. Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/craftcore.git
    ```
2. Abra o projeto no Visual Studio 2022.

1. 3. Compile o projeto diretamente no Visual Studio.
      
4. Configure o servidor:
    - As configurações do servidor estão no arquivo CraftCore.yml, com instruções detalhadas dentro do arquivo.

5. Execute o servidor:
    - Após compilar, basta rodar o arquivo executável gerado.

## Estrutura do Projeto

- Protocolo: Implementa parte do protocolo do Minecraft, lidando com status e login dos jogadores.
- Extensibilidade: O servidor é de fácil modificação e será projetado para suportar plugins e minigames no futuro.
- Configuração: Configurações do servidor são feitas via um arquivo CraftCore.yml em formato YAML.

## Planos Futuros

- Implementação completa do protocolo do Minecraft.
- Criação de um framework robusto para minigames.
- Suporte a transferência de jogadores entre servidores (server transfer).
- Sistema de plugins para extensões e customizações.

## Contribuição

Atualmente, não há um guia formal para contribuições. No entanto, sinta-se à vontade para enviar pull requests ou abrir issues. Tenha senso-comum ao contribuir e mantenha o foco em melhorias e estabilidade.

## Licença

Este projeto é licenciado sob a Licença MIT. Leia o arquivo LICENSE.TXT para saber mais.