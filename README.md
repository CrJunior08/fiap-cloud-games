# 🎮 FCG - Games Service

Microsserviço responsável pela gestão e busca de jogos da **FIAP Cloud Games (FCG)**.  
Este módulo implementa **CRUD completo de jogos**, **indexação e busca com Elasticsearch**, e integra-se aos demais microsserviços de **Usuários** e **Pagamentos** via API Gateway.

---

## 🚀 Objetivo

O objetivo deste microsserviço é fornecer uma API independente e escalável para:
- Gerenciar jogos (cadastro, atualização, exclusão e listagem).
- Indexar e buscar jogos de forma inteligente via **Elasticsearch**.
- Suportar recomendações e métricas de popularidade baseadas em agregações.
- Servir como base de consulta para os demais módulos do sistema **FIAP Cloud Games**.

---

## 🧱 Arquitetura

A aplicação segue o padrão **Clean Architecture / DDD**, dividida em:

```text
src/
├── FCG.Games.Api/            → Endpoints e Controllers
├── FCG.Games.Application/    → Casos de uso (services, handlers)
├── FCG.Games.Domain/         → Entidades e interfaces
├── FCG.Games.Infrastructure/ → Repositórios, Elasticsearch e persistência
└── FCG.Games.Tests/          → Testes unitários e de integração




## 🧩 Principais Tecnologias

.NET 8 (C#)

ASP.NET Core Web API

Elasticsearch 8.x

Docker (opcional)

Swagger (documentação automática)

Logger (Serilog ou integrado)

---

## ⚙️ Configuração do Ambiente

### 🔧 Requisitos
- .NET SDK 8+
- Elasticsearch rodando localmente (ou via Docker)
- Postman (ou ferramenta REST de sua escolha)

### 🐳 Subindo o Elasticsearch (opcional via Docker)
```bash
docker run -d --name elasticsearch `
  -p 9200:9200 `
  -e "discovery.type=single-node" `
  -e "xpack.security.enabled=false" `
  docker.elastic.co/elasticsearch/elasticsearch:8.15.0



Verifique se está rodando:

curl http://localhost:9200



⚙️ Configuração do Índice games

Crie o índice manualmente no Postman ou via curl:
PUT http://localhost:9200/games
Content-Type: application/json

{
  "settings": { "number_of_shards": 1, "number_of_replicas": 0 },
  "mappings": {
    "properties": {
      "id":         { "type": "keyword" },
      "title":      { "type": "text" },
      "genre":      { "type": "keyword" },
      "platform":   { "type": "keyword" },
      "description":{ "type": "text" },
      "rating":     { "type": "float" },
      "releasedAt": { "type": "date" }
    }
  }
}



▶️ Execução do Projeto

Na pasta raiz do projeto:
dotnet build src/FCG.Games.Api/FCG.Games.Api.csproj
dotnet run   --project src/FCG.Games.Api/FCG.Games.Api.csproj


Por padrão, a API roda em:
http://localhost:5201


Acesse o Swagger:
http://localhost:5201/swagger


🧪 Testes no Postman
Criar jogo
POST http://localhost:5201/api/games/create
Content-Type: application/json

{
  "name": "The Legend of Zelda: Tears of the Kingdom",
  "genre": "Adventure",
  "platform": "Nintendo Switch",
  "description": "Explore um vasto mundo aberto e use a criatividade para resolver desafios.",
  "rating": 9.8,
  "releasedAt": "2023-05-12"
}


Listar jogos
GET http://localhost:5201/api/games


Buscar jogos por título/descrição no Elasticsearch
GET http://localhost:5201/search/games?q=Zelda


Atualizar jogo
PUT http://localhost:5201/api/games/1
Content-Type: application/json

{
  "id": "1",
  "name": "Zelda TOTK",
  "genre": "Adventure"
}



Remover jogo
DELETE http://localhost:5201/api/games/1


🧠 Elasticsearch: Consultas e Métricas
Busca simples
GET http://localhost:9200/games/_search?q=Zelda


Consulta avançada
POST http://localhost:9200/games/_search
{
  "query": {
    "multi_match": {
      "query": "RPG",
      "fields": [ "title^2", "description" ]
    }
  }
}


Agregação: jogos mais bem avaliados
POST http://localhost:9200/games/_search
{
  "size": 0,
  "aggs": {
    "top_rated": {
      "terms": { "field": "genre" },
      "aggs": { "avg_rating": { "avg": { "field": "rating" } } }
    }
  }
}



🛡️ Segurança e Logs

Autenticação e autorização podem ser integradas via API Gateway.
Requisições e respostas são logadas para auditoria.
Erros são tratados globalmente com middleware customizado.


🧩 Integrações

Serviço	Função principal
Users Service	Autenticação e perfis
Payments Service	Transações e status de compra
Elasticsearch	Busca, métricas e recomendações
API Gateway	Roteamento, autenticação e monitoramento central


🧰 Variáveis de Ambiente

Variável	Descrição
ELASTIC_URI	URL de conexão com o Elasticsearch (ex: http://localhost:9200
)
ASPNETCORE_ENVIRONMENT	Ambiente de execução (Development / Production)
LOG_LEVEL	Nível de log desejado (Information, Warning, Error)


📦 Deploy (exemplo local)

dotnet publish src/FCG.Games.Api/FCG.Games.Api.csproj -c Release -o ./publish


Na cloud (Azure / AWS):
Utilize Serverless Framework ou CLI da plataforma.

Configure as funções e o API Gateway apontando para o Games Service.

























