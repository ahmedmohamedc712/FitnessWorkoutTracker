# Fitness Workout Tracker

# Description

A production-oriented ASP.NET Core Web API for managing workouts, scheduling training sessions, and tracking exercise progress.

Built with **Clean Architecture**, **FastEndpoints**, **Entity Framework Core**, **SQL Server**, **JWT Authentication**, the **Specification Pattern**, and **Integration Testing** with rich **domain modeling**.

# Why?

As business requirements evolve, backend systems become increasingly difficult to maintain when business logic is spread across multiple layers. This project demonstrates an architectural approach that keeps responsibilities well-defined and allows the domain model to evolve without leaking implementation details into the rest of the application.

# Quick Start

## Prerequisites

- .NET 10 SDK
- SQL Server
- Docker Desktop (required to run the integration tests using Testcontainers)

## Clone the repository

```bash
git clone https://github.com/ahmedmohamedc712/FitnessWorkoutTracker.git
cd FitnessWorkoutTracker
```

## Restore NuGet packages

```bash
dotnet restore
```

## Configure the database


Update the connection string in:

```text
src/PublicApi/appsettings.json
```

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your Connection String"
  }
}
```
Apply the database migrations

Create the database and apply all Entity Framework Core migrations:

```bash
dotnet ef database update --project src/Infrastructure --startup-project src/PublicApi
```

Note: Ensure the dotnet-ef tool is installed. If it isn't, install it with:
```bash
dotnet tool install --global dotnet-ef
```

## Configure JWT settings

Use **user-secrets** or **environment variables** to configure JWT settings. Note that **user-secrets are only available in the Development environment.**
```bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:SigningKey" "AddYourSuperSecretKey"
dotnet user-secrets set "Jwt:LifeTime" 60
```

**Note:** LifeTime is in minutes

## Build

```bash
dotnet build FitnessWorkoutTracker.slnx
```

## Run

```bash
dotnet run --project src/PublicApi/PublicApi.csproj
```

Swagger/OpenAPI documentation is available automatically in Development mode.

---

# Usage

## Authentication

Authenticate using:

- `POST /api/auth/signup`
- `POST /api/auth/login`

Use the returned JWT bearer token to access protected endpoints.

## Highlights

### Business

- Workout management
- Exercise management
- Workout scheduling
- Exercise progress tracking

### Engineering

- Rich Domain Model
- Clean Architecture
- JWT Authentication
- Specification Pattern
- Pagination, Filtering & Sorting
- FluentValidation
- Swagger / OpenAPI
- Health Checks
- Integration Testing with Testcontainers

## Health Check

```text
GET /health
```

## Running Tests

Ensure Docker Desktop is running before executing the integration tests.

```bash
dotnet test tests/PublicApiIntegrationTests/PublicApiIntegrationTests.csproj
```

---

# Contributing

Contributions are welcome.

If you'd like to improve the project:

1. Fork the repository.
2. Create a feature branch.
3. Make your changes.
4. Add or update tests where appropriate.
5. Submit a pull request.

Please keep changes consistent with the project's architectural principles:

- Preserve Clean Architecture boundaries.
- Keep business rules inside the Domain layer.
- Avoid introducing an anemic domain model.
- Maintain comprehensive test coverage for new functionality.
