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