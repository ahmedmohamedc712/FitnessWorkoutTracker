# Fitness Workout Tracker

# Description

A production-oriented ASP.NET Core Web API for managing workouts, scheduling training sessions, and tracking exercise progress.

Built with **Clean Architecture**, **FastEndpoints**, **Entity Framework Core**, **SQL Server**, **JWT Authentication**, the **Specification Pattern**, and **Integration Testing**, the project showcases how business rules can be encapsulated within rich domain models instead of procedural service logic.

---

# Why ?

Many applications implement business logic through service methods while entities act as simple data containers. This project follows a rich domain model instead, where domain entities own their behavior and enforce business rules. The Application layer orchestrates use cases, while the Domain layer protects its own invariants, resulting in a more expressive and maintainable design.

---

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

## Configure JWT settings

Use **user-secrets** or **Environment variables** but keep in mind that user-secrets only used in **Development Environment**. Example:
**user-secrets:**

```json
{
  "Jwt": {
    "SigningKey": "AddYourSuperSecretKey",
    "LifeTime": 60
  }
}
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

## Capabilities

- Workout management
- Exercise management
- Scheduled workout management
- Exercise progress tracking
- Pagination
- Filtering
- Sorting

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
