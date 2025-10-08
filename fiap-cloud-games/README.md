🎮 FIAP Cloud Games (FCG)

Plataforma de venda de jogos digitais e gerenciamento de biblioteca de jogos adquiridos, voltada para educação em tecnologia. 

---

📌 Objetivos

- Criar uma API REST em .NET 8.
- Implementar cadastro e autenticação de usuários.
- Gerenciar biblioteca de jogos adquiridos por cada usuário.
- Adotar práticas de DDD, TDD/BDD, logs estruturados e documentação via Swagger.
- Servir de fundação para futuras fases que incluem matchmaking e gestão de servidores.

---

🧱 Arquitetura

- Tipo: Monolítica (foco em agilidade na fase inicial)
- Backend: .NET 8 + ASP.NET Core (Controllers MVC)
- ORM: Entity Framework Core
- Autenticação: JWT (Json Web Token)
- Padrão de Projeto: Domain-Driven Design (DDD)

---

🔐 Perfis de Acesso

- Usuário (Default): Acesso à plataforma e biblioteca de jogos adquiridos.
- Administrador: Acesso total, incluindo cadastro de jogos, administração de usuários e criação de promoções.

---

✅ Funcionalidades

👥 Usuários
- Cadastro com nome, e-mail e senha segura (mín. 8 caracteres, com letras, números e caracteres especiais)
- Validação de e-mail e senha
- Autenticação JWT
- Diferenciação de perfis (User/Admin)

🎮 Jogos
- Cadastro e associação de jogos adquiridos ao usuário (disponível para perfil Admin)
- Listagem de biblioteca de jogos por usuário

---

🛠️ Tecnologias Utilizadas

| Tecnologia                           | Finalidade                                 |
|-------------------------------------|-------------------------------------------|
| .NET 8                                | Backend/API                              |
| ASP.NET Core                   | Desenvolvimento Web               |
| Entity Framework Core      | Persistência com Migrations      |
| Swagger (Swashbuckle)    | Documentação da API               |
| JWT                                   | Autenticação e Autorização        |
| xUnit / NUnit / BDDfy         | Testes Unitários e/ou BDD         |
| FluentValidation                 | Validações                                  |
| Docker                           | Containerização                             |
| Azure DevOps                     | Pipelines CI/CD                            |


---

<h2>📁 Estrutura do Projeto</h2>

<pre><code>
FCG/
│
├── FCG.API/              API principal (.NET 8)
│   ├── Controllers/       Endpoints RESTful
│   ├── Middlewares/       Tratamento de exceções e logs
│   ├── Program.cs         Configuração principal
│   └── appsettings.json   Configurações da aplicação
│
├── FCG.Domain/           Entidades e regras de negócio (DDD)
│   └── Entities/          User, Game
│
├── FCG.Application/      Casos de uso (Application Layer)
│
├── FCG.Infra/            Repositórios e contexto EF
│   └── Migrations/        Scripts gerados pelo EF Core
│
├── FCG.Tests/            Testes unitários e BDD
│
└── README.md
</code></pre>

---

🔧 Como Rodar o Projeto

1.Clone este repositório:,
git clone https://github.com/CrJunior08/fiap-cloud-games.git
cd fiap-cloud-games
 
 2. Configure o banco de dados no appsettings.json. Exemplo para SQL Server:,
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=FCG_DB;User Id=sa;Password=SuaSenhaForteAqui;TrustServerCertificate=True;"
}

3.Crie o banco de dados manualmente (opcional):,
Acesse o SQL Server Management Studio (SSMS).
Execute:
CREATE DATABASE FCG_DB;

4. Execute as migrations:,
dotnet ef database update

5. Rode a aplicação:,
dotnet run --project FCG.API

Acesse a documentação Swagger:,
http://localhost:{porta}/swagger

---

🧪 Testes

●	Execute os testes com:
dotnet test

TDD ou BDD aplicados no módulo de autenticação e cadastro de usuário.

---

🧠 Event Storming

Documentação disponível no Miro contendo: https://miro.com/app/board/uXjVI0KTeKY=/

●	Fluxo de Criação de Usuário

●	Fluxo de Criação de Jogos

●	Cores e domínios conforme DDD (Commands, Events, Aggregates)

--- 

🐳 Docker

Build e execução local com Docker: 

docker build -t fcg-api:latest .
docker run -d -p 8080:80 fcg-api:latest

---

🔄 CI/CD (Azure DevOps)

●	CI: Build e Testes automáticos a cada commit/PR (azure-pipelines.yml)

●	CD: Deploy automatizado após merge na main (cd-pipeline.yml)

●	Executado com agente local configurado com Docker Desktop
