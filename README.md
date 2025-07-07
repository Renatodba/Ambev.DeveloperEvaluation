`READ CAREFULLY`
# Ambev Developer Evaluation Project

This repository contains the complete solution for the Sales API as part of the Ambev Developer Evaluation. Below you will find instructions to configure, run, and test the project.

---

## Table of Contents

- [Prerequisites](#prerequisites)
- [Cloning the Repository](#cloning-the-repository)
- [Docker Setup](#docker-setup)
- [Database](#database)
- [Building and Running the API](#building-and-running-the-api)
- [Migrations and Database Update](#migrations-and-database-update)
- [Testing](#testing)
- [Project Structure](#project-structure)
- [Main Endpoints](#main-endpoints)
- [Contact](#contact)

---

## Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) & [Docker Compose](https://docs.docker.com/compose/)
- [Git](https://git-scm.com/)

---

## Cloning the Repository

```bash
git clone https://github.com/YOUR_USERNAME/Ambev.DeveloperEvaluation.git
cd Ambev.DeveloperEvaluation/template/backend
```

---

## Docker Setup

In the `template/backend` directory, run:

```bash
docker-compose up -d
```

This will start:

- PostgreSQL at `localhost:5432`
- MongoDB at `localhost:27017`
- Redis at `localhost:6379`
- Adminer at `localhost:8082`
- Web API at `localhost:8080` (HTTP) and `8081` (HTTPS)

---

## Database

1. Access Adminer at [http://localhost:8082](http://localhost:8082):

   - System: PostgreSQL
   - Server: `ambev_developer_evaluation_database`
   - Username: `developer`
   - Password: `ev@luAt10n`
   - Database: `developer_evaluation`

2. Verify tables after running migrations.

---

## Building and Running the API

Navigate to `template/backend/src/Ambev.DeveloperEvaluation.WebApi`:

```bash
dotnet clean
dotnet restore
dotnet build
dotnet run --urls "http://*:8080;https://*:8081"
```

The API is available at `http://localhost:8080`.

---

## Migrations and Database Update

Migrations are in the **ORM** project:

```bash
cd src/Ambev.DeveloperEvaluation.ORM

# Add a new migration
dotnet ef migrations add MigrationName --startup-project ../Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj

# Apply migrations
dotnet ef database update --startup-project ../Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj
```

> If `dotnet ef` is not recognized, install the global tool:
>
> ```bash
> dotnet tool install --global dotnet-ef --version 8.0.10
> ```

---

## Testing

### Unit Tests

```bash
dotnet test tests/Ambev.DeveloperEvaluation.Unit
```

### Functional Integration Tests

```bash
dotnet test tests/Ambev.DeveloperEvaluation.Functional
```

> Ensure containers are running (`docker-compose up -d`) before running integration tests.

---

## Project Structure

```
template/backend/
├── src/
│   ├── Ambev.DeveloperEvaluation.WebApi/   # ASP.NET Core Web API project
│   ├── Ambev.DeveloperEvaluation.Common/   # Shared abstractions and utilities
│   ├── Ambev.DeveloperEvaluation.Domain/   # Domain entities and business logic
│   ├── Ambev.DeveloperEvaluation.ORM/      # EF Core context and configurations
│   └── Ambev.DeveloperEvaluation.IoC/      # Dependency injection
└── tests/
    ├── Ambev.DeveloperEvaluation.Unit/     # Unit tests
    └── Ambev.DeveloperEvaluation.Functional # Functional integration tests
```

---

## Main Endpoints

Access Swagger at `/swagger/index.html`:

- **Sales**
  - `GET /api/sales`        – List sales
  - `GET /api/sales/{id}`   – Get sale by ID
  - `POST /api/sales`       – Create new sale
  - `PUT /api/sales/{id}`   – Update sale
  - `DELETE /api/sales/{id}`– Cancel sale

---

