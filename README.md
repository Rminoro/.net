# Integrantes
98974 - Ana Júlia Almeida Silva Neves
552410 - NICOLY OLIVEIRA SANTOS
99988 - Rafael Minoro Itokazo
551831 - Vitor da Silva Pereira




# Sprint4dotnet API

Esta é uma API desenvolvida em .NET que implementa funcionalidades de recomendação utilizando ML.NET e integra serviços externos para fornecer dados climáticos.

## Funcionalidades

- **Integração com serviços externos**: A API se conecta à API do OpenWeather para obter dados climáticos.
- **Recomendação de produtos**: Utiliza ML.NET para gerar recomendações com base nas avaliações de produtos.

## Testes Implementados

Utilizamos o xUnit para implementar testes unitários que garantem a funcionalidade da API. Os principais testes incluem:

- Testes de registro de clientes.
- Testes de recomendações de produtos.

## Práticas de Clean Code

Aplicamos princípios de Clean Code e SOLID, incluindo:

- **Single Responsibility Principle**: Cada classe tem uma única responsabilidade.
- **Dependency Injection**: Utilizamos injeção de dependência para promover a testabilidade e reduzir o acoplamento.

## Endpoints da API

### ClientsController
- **POST /api/clients/register**: Registra um novo cliente. É necessário fornecer o email, senha e nome do cliente no corpo da requisição.
- **PUT /api/clients/{id}**: Atualiza os dados de um cliente existente, onde `{id}` é o ID do cliente.
- **DELETE /api/clients/{id}**: Remove um cliente pelo ID especificado.

### ClimaController
- **GET /clima/{cidade}**: Retorna os dados climáticos da cidade especificada. A cidade é passada como um parâmetro na URL.

### RecomendacaoController
- **POST /recomendacao/treinar**: Treina o modelo de recomendação. Este endpoint deve ser chamado antes de utilizar as recomendações.
- **GET /recomendacao/recomendar/{cpf}/{produto}**: Retorna uma recomendação para um produto baseado no CPF do cliente e no produto solicitado.

## Como Executar

1. Clone o repositório.
2. Restaure os pacotes com `dotnet restore`.
3. Execute a aplicação com `dotnet run`.

## Contribuições

Sinta-se à vontade para contribuir com melhorias ou abrir issues.
