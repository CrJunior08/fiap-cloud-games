# 🕹️ FIAP Cloud Games — Games Service

Microsserviço de jogos do ecossistema FIAP Cloud Games (FCG).  
Responsável por CRUD de jogos, indexação e busca via Elasticsearch, servindo outros microsserviços como ponto de consulta.

---

## 🎯 Objetivos

- Permitir criação, leitura, atualização e remoção de jogos.  
- Indexar os jogos no Elasticsearch para permitir buscas inteligentes (multi-match, agregações, filtros).  
- Servir como backend consultável pelos demais microsserviços (Users, Payments, etc).  
- Gerar métricas ou relatórios de usos/popularidade via agregações no Elasticsearch.  

---

## 🧱 Arquitetura & Organização

Estrutura típica com Clean Architecture / DDD:
src/
├── FCG.Games.Api/ → Endpoints, Controllers, Interfaces HTTP
├── FCG.Games.Application/ → Casos de uso, serviços de aplicação
├── FCG.Games.Domain/ → Entidades, regras de negócio, interfaces
├── FCG.Games.Infrastructure/ → Repositórios, Elasticsearch, persistência
└── FCG.Games.Tests/ → Testes unitários / integração



Tecnologias esperadas:

- .NET / C#  
- ASP.NET Core Web API  
- Elasticsearch  
- Docker (para ambiente local)  
- Swagger / OpenAPI  
- Logging  

---

## 🔄 Fluxo de Comunicação entre Microsserviços

Aqui vai o fluxo ideal de chamadas num cenário completo:

[Client / Frontend]
↓ HTTP / REST
[API Gateway] → (roteamento + autenticação)
↓
[Users Service] — autentica / fornece token / dados de perfil
↓ (token / autorização embutida)
[Games Service] — recebe requisições de jogos e buscas
↳ (internamente) → consulta / indexa no Elasticsearch
↓ resultado
[Payments Service] — para operações de compra / status




- O Gateway distribui as requisições para o serviço apropriado.  
- O Games Service só lida com dados de jogos e consultas.  
- Para buscas, ele consulta o Elasticsearch e retorna resultados filtrados / ordenados.  
- Quando há atualizações de jogos, o Games Service também reindexa ou atualiza no Elasticsearch.  
- Os serviços podem interagir entre si via APIs REST internas autenticadas ou por eventos (se implementado).

Você pode representar isso com **Mermaid**, **PlantUML** ou imagem gráfica e colocar no README ou dentro de `docs/`.

### Exemplo de diagrama em **Mermaid** (para inserir no README):

```mermaid
sequenceDiagram
    participant Client
    participant Gateway
    participant Users
    participant Games
    participant Payments
    Client->>Gateway: requisição (ex: buscar jogo)
    Gateway->>Users: validar token / autenticação
    Users-->>Gateway: validação OK (ou falha)
    Gateway->>Games: encaminha requisição de jogos
    Games->>Elasticsearch: consulta / busca
    Elasticsearch-->>Games: resultado
    Games-->>Gateway: retorna resposta
    Gateway-->>Client: envia resposta ao cliente


⚙️ Configuração do Ambiente
Requisitos

.NET SDK compatível

Elasticsearch rodando localmente ou via container

Ferramenta REST (Postman / Insomnia)

(Opcional) Docker

Docker para Elasticsearch
docker run -d --name elasticsearch \
  -p 9200:9200 \
  -e "discovery.type=single-node" \
  -e "xpack.security.enabled=false" \
  docker.elastic.co/elasticsearch/elasticsearch:8.15.0


Verifique:

curl http://localhost:9200



Variáveis de Ambiente (exemplos)
Variável	Descrição	Exemplo
ELASTIC_URI	URL de conexão com Elasticsearch	http://localhost:9200
ASPNETCORE_ENVIRONMENT	Ambiente (Development / Production)	Development
LOG_LEVEL	Nível de log	Information, Warning, etc.
Games_Db_ConnectionString	String de conexão com banco relacional (se aplicável)	Server=.;Database=Games;User=sa;Password=XXX


▶️ Executar Localmente

1. Garanta que o Elasticsearch esteja rodando.

2. Na raiz do projeto, execute:
dotnet build src/FCG.Games.Api/FCG.Games.Api.csproj
dotnet run --project src/FCG.Games.Api/FCG.Games.Api.csproj

3. A API por padrão rodará em algo como http://localhost:5000 ou similar.

4. Acesse /swagger para ver a documentação interativa.


🧪 Endpoints Principais

Aqui vão exemplos genéricos (verifique seu código para rotas exatas):

Método	Rota	Função
GET	/api/games	Listar todos os jogos
GET	/api/games/{id}	Obter um jogo por ID
POST	/api/games	Criar um novo jogo
PUT	/api/games/{id}	Atualizar jogo
DELETE	/api/games/{id}	Deletar jogo
GET	/search/games?q={termo}	Buscar jogos por termo (Elasticsearch)


Testes

Dentro de FCG.Games.Tests, você deve ter testes unitários (lógicas isoladas) e testes de integração (com o Elasticsearch, repositórios reais).

Execute com:
dotnet test


📂 Estrutura e Notas Adicionais

FCG.Games.Api → camada de entrada HTTP

FCG.Games.Application → casos de uso

FCG.Games.Domain → entidades, interfaces, regras

FCG.Games.Infrastructure → implementação concreta (banco, Elasticsearch)

FCG.Games.Tests → testes

Pode haver pasta docs/ para diagramas visuais ou documentos auxiliares


























