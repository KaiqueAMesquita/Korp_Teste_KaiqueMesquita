# Korp - Sistema de Emissão de Notas Fiscais

**Versão:** 1.0.0  
**Frontend:** Angular 19  
**Backend:** ASP.NET Core Web API (.NET 9)  
**Banco de dados:** PostgreSQL

Projeto desenvolvido para o teste técnico da Korp. A solução implementa um fluxo simplificado de cadastro de produtos, criação de notas fiscais e finalização/impressão com atualização de estoque, utilizando dois serviços independentes e um frontend Angular.

> Esta versão representa o fluxo funcional solicitado no teste técnico. Não realiza emissão fiscal oficial, integração com SEFAZ, geração de XML, cálculo de impostos ou regras fiscais reais.

## Funcionalidades da versão 1.0.0

- Cadastro, edição, consulta e listagem de produtos.
- Controle de código, descrição e saldo dos produtos.
- Criação de notas fiscais com múltiplos produtos e quantidades.
- Numeração sequencial definida no backend.
- Status da nota: `Opened` e `Closed`.
- Consulta da lista e dos detalhes das notas fiscais.
- Finalização/impressão somente de notas abertas.
- Indicador visual de processamento durante a impressão.
- Débito automático do estoque conforme as quantidades da nota.
- Transação com rollback no débito de estoque.
- Tratamento de indisponibilidade do serviço de estoque.
- Feedback de erro e sucesso no frontend.
- Persistência com Entity Framework Core e PostgreSQL.
- OpenAPI/Swagger UI nas duas APIs em ambiente de desenvolvimento.

## Arquitetura

A aplicação é dividida em três projetos:

```text
Angular Frontend
      |
      +--------------------+
      |                    |
      v                    v
  StockApi             BillingApi
      ^                    |
      |                    |
      +------ HTTP --------+
      |                    |
      v                    v
 PostgreSQL            PostgreSQL
  estoque              faturamento
```

### StockApi

Responsável pelo cadastro dos produtos e pelo controle dos saldos. O débito de múltiplos itens é executado em transação: caso um item falhe, a operação é revertida.

### BillingApi

Responsável pela criação, consulta e finalização das notas fiscais. Durante a criação da nota, consulta o StockApi para obter os dados do produto. Durante a impressão, chama o StockApi para debitar o estoque e somente depois altera a nota para `Closed`.

### Frontend Angular

Responsável pela interface do usuário e pela comunicação com as APIs.

Rotas principais:

```text
/products
/invoices
/invoices/create
/invoices/:id
```

## Tecnologias utilizadas

### Backend

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core 9
- Npgsql Entity Framework Core Provider
- PostgreSQL
- OpenAPI / Swagger UI
- LINQ
- HttpClient

### Frontend

- Angular 19
- TypeScript
- RxJS 7.8
- Reactive Forms
- Bootstrap 5.3
- Font Awesome Free 7
- SCSS

## Estrutura do repositório

```text
Korp_Teste_SeuNome/
|
|-- StockApi/
|-- BillingApi/
|-- frontend/
|-- README.md
```

## Pré-requisitos

Para executar o projeto localmente:

- .NET SDK 9
- PostgreSQL
- Node.js e npm compatíveis com Angular 19
- ferramenta `dotnet-ef` versão 9.x

Caso `dotnet ef` não esteja disponível:

```bash
dotnet tool install --global dotnet-ef --version 9.*
```

# Banco de dados e User Secrets

A solução utiliza dois `DbContext` independentes, um para estoque e outro para faturamento. Para reproduzir a separação dos serviços, podem ser criados dois bancos PostgreSQL:

```sql
CREATE DATABASE stock_db;
CREATE DATABASE billing_db;
```

## Por que usar User Secrets?

As APIs leem a connection string através de:

```csharp
builder.Configuration.GetConnectionString("DefaultConnection")
```

Os projetos `StockApi` e `BillingApi` possuem `UserSecretsId` configurado no arquivo `.csproj`. Dessa forma, a connection string usada no desenvolvimento pode ser armazenada localmente pelo .NET, sem colocar usuário e senha do PostgreSQL nos arquivos `appsettings.json` versionados no Git.

Os arquivos `appsettings.json` desta versão não contêm a connection string do PostgreSQL.

> O User Secrets é utilizado somente para configuração local de desenvolvimento. A senha real do banco não deve ser escrita no README nem enviada ao repositório.

## Configurando o StockApi

Entre na pasta:

```bash
cd StockApi
```

Configure a connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=stock_db;Username=postgres;Password=SUA_SENHA"
```

Para verificar a configuração local:

```bash
dotnet user-secrets list
```

Restaure dependências e aplique a migration:

```bash
dotnet restore
dotnet ef database update
```

Execute utilizando o perfil HTTP:

```bash
dotnet run --launch-profile http
```

Endereço esperado:

```text
http://localhost:5001
```

Swagger UI em desenvolvimento:

```text
http://localhost:5001/swagger
```

## Configurando o BillingApi

Em outro terminal:

```bash
cd BillingApi
```

Configure a connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=billing_db;Username=postgres;Password=SUA_SENHA"
```

Para verificar:

```bash
dotnet user-secrets list
```

Restaure dependências e aplique a migration:

```bash
dotnet restore
dotnet ef database update
```

Execute:

```bash
dotnet run --launch-profile http
```

Endereço esperado:

```text
http://localhost:5002
```

Swagger UI em desenvolvimento:

```text
http://localhost:5002/swagger
```

O endereço do StockApi usado pelo BillingApi está em `BillingApi/appsettings.Development.json`:

```json
{
  "Services": {
    "StockApi": "http://localhost:5001"
  }
}
```

Essa configuração é apenas o endereço interno do serviço e não contém credenciais.

# Executando o frontend

Em outro terminal:

```bash
cd frontend
npm install
npm start
```

Endereço padrão:

```text
http://localhost:4200
```

As URLs das APIs estão configuradas nos arquivos:

```text
src/environments/environment.ts
src/environments/environment.development.ts
```

Configuração da versão 1.0.0:

```typescript
export const environment = {
  production: false,
  stockApiUrl: 'http://localhost:5001/api',
  billingApiUrl: 'http://localhost:5002/api'
};
```

> Caso a porta do frontend seja alterada, a origem permitida pela política CORS dos dois backends também deve ser atualizada.

## Ordem recomendada de execução

1. Iniciar o PostgreSQL.
2. Configurar os User Secrets das duas APIs.
3. Aplicar as migrations.
4. Executar o StockApi na porta `5001`.
5. Executar o BillingApi na porta `5002`.
6. Executar o frontend Angular na porta `4200`.
7. Acessar `http://localhost:4200`.

# Endpoints

## StockApi

| Método | Endpoint | Finalidade |
|---|---|---|
| GET | `/api/Product` | Listar produtos |
| GET | `/api/Product/{id}` | Consultar produto por ID |
| POST | `/api/Product` | Cadastrar produto |
| PUT | `/api/Product/{id}` | Atualizar produto |
| POST | `/api/stock/debit` | Debitar múltiplos itens do estoque |

Exemplo de cadastro:

```json
{
  "code": "PROD-001",
  "description": "Produto de exemplo",
  "balance": 10
}
```

Exemplo de débito:

```json
{
  "items": [
    {
      "productId": "00000000-0000-0000-0000-000000000000",
      "quantity": 2
    }
  ]
}
```

O endpoint de débito retorna `204 No Content` quando concluído com sucesso.

## BillingApi

| Método | Endpoint | Finalidade |
|---|---|---|
| GET | `/api/Invoice` | Listar notas fiscais |
| GET | `/api/Invoice/{id}` | Consultar nota e seus itens |
| POST | `/api/Invoice` | Criar uma nota fiscal |
| PUT | `/api/Invoice/{id}` | Atualizar o status da nota |
| POST | `/api/Invoice/{id}/print` | Finalizar/imprimir a nota e debitar o estoque |

Exemplo de criação:

```json
{
  "items": [
    {
      "productId": "00000000-0000-0000-0000-000000000000",
      "quantity": 2
    },
    {
      "productId": "11111111-1111-1111-1111-111111111111",
      "quantity": 1
    }
  ]
}
```

A numeração sequencial, o status inicial `Opened` e a data de criação são definidos pelo backend.

# Fluxo principal

```text
Cadastrar produto
      |
      v
Criar nota com um ou mais produtos
      |
      v
BillingApi consulta o StockApi
      |
      v
Nota é salva como Opened
      |
      v
Usuário solicita impressão
      |
      v
BillingApi chama /api/stock/debit
      |
      v
StockApi valida e debita em transação
      |
      v
BillingApi altera a nota para Closed
      |
      v
Frontend apresenta sucesso
```

# CORS

Os dois backends possuem a política CORS `Angular`, permitindo requisições do frontend em:

```text
http://localhost:4200
```

A comunicação do BillingApi com o StockApi é feita diretamente entre servidores através de `HttpClient` e não depende de CORS.

# Build

Backend:

```bash
cd StockApi
dotnet build
```

```bash
cd BillingApi
dotnet build
```

Frontend:

```bash
cd frontend
npm install
npm run build
```