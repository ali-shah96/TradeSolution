# Trading Microservice

Minimal trading microservice demonstrating:
- Execute & store trades (EF Core / SQL Server)
- Retrieve trades (paged + by id)
- Publish trade events (MassTransit → RabbitMQ)
- Separate console consumer logging received trades
- Dockerized services

## Tech
.NET 8, C# 12, ASP.NET Core, EF Core (Code First), SQL Server, MassTransit, RabbitMQ, AutoMapper, Docker.

## Prerequisites
- .NET 8 SDK
- Docker Desktop
- SQL Server (local instance or add a container)
- (Optional) EF tools: `dotnet tool install -g dotnet-ef`

## Quick Start (Docker, using host SQL Server)
1. Ensure SQL Server is listening on port 1433 and login exists (example):
   - User: `tradinguser` / Password: `tradinguser`
2. Add (or adjust) environment variable in `docker-compose.override.yml`:

Run `database update` again whenever pointing to a new empty database. Create a new `migrations add <Name>` only when the model changes.

## Demo Flow Suggestion
1. Start stack: `docker compose up --build`
2. Open Swagger: http://localhost:8080/swagger
3. POST /api/v1/trades (e.g. `{ "symbol":"AAPL","quantity":5,"price":187.42 }`)
4. Check RabbitMQ UI (http://localhost:15673) → Queues → `trade_queue` (message count will increment / flow)
5. Observe `trading.consumer` container logs: should show `Trade received: AAPL, Amount: 187.42`
6. (Optional) Query DB `TradingDb.dbo.Trades` to confirm persistence.

## Sample curl
curl -X POST http://localhost:8080/api/v1/trades ^ -H "Content-Type: application/json" ^ -d "{"symbol":"AAPL","quantity":5,"price":187.42}"

## Simple Flow Diagram
Client -> API (/trades) -> EF Core -> SQL Server 
-> MassTransit -> RabbitMQ -> Consumer (logs)