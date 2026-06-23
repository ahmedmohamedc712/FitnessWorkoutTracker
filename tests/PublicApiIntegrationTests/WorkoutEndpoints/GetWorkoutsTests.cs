using Application.Abstraction;
using Application.Features.Workouts.GetAll;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using PublicApiIntegrationTests.Extensions;
using PublicApiIntegrationTests.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PublicApiIntegrationTests.WorkoutEndpoints;

public class GetWorkoutsTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private const string TimeZoneHeader = "X-TimeZone";

    private readonly CustomWebApplicationFactory _factory;
    private HttpClient? _client = default;
    public GetWorkoutsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }
    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync();
    }
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetAllWorkouts_WithValidRequest_ReturnsOk()
    {
        // Arrange
        var user = DataSeedHelper.CreateUser();
        await _factory.SeedAsync(async dbContext =>
        {
            var workout = new Workout("Morning Run", "5km run", user.Id);

            await dbContext.Users.AddAsync(user);

            await dbContext.Workouts.AddAsync(workout);
            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync("api/workouts?page=1&pageSize=10");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var workoutsResponse = await response.Content.ReadFromJsonAsync<GetWorkoutsResponse>();
        Assert.NotNull(workoutsResponse);

        Assert.Single(workoutsResponse.Workouts);
    }

    [Fact]
    public async Task GetAllWorkouts_WithMultipleWorkouts_ReturnsPaginatedResults()
    {
        // Arrange
        var user = DataSeedHelper.CreateUser();

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user);

            for (int i = 1; i <= 5; i++)
            {
                var workout = new Workout($"Workout {i}", $"Description {i}", user.Id);
                await dbContext.Workouts.AddAsync(workout);
            }

            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync("api/workouts?page=1&pageSize=2");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var workoutsResponse = await response.Content.ReadFromJsonAsync<GetWorkoutsResponse>();
        Assert.NotNull(workoutsResponse);

        Assert.Equal(2, workoutsResponse.Workouts.Count());
    }

    [Fact]
    public async Task GetAllWorkouts_WithSearchTerm_FiltersWorkouts()
    {
        // Arrange
        var user = DataSeedHelper.CreateUser();
        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user);

            var workout1 = new Workout("Morning Run", "5km run", user.Id);
            var workout2 = new Workout("Gym Session", "Weight lifting", user.Id);
            var workout3 = new Workout("Running Marathon", "10km race", user.Id);

            await dbContext.Workouts.AddAsync(workout1);
            await dbContext.Workouts.AddAsync(workout2);
            await dbContext.Workouts.AddAsync(workout3);

            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync("api/workouts?page=1&pageSize=10&searchTerm=Running");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var workoutsResponse = await response.Content.ReadFromJsonAsync<GetWorkoutsResponse>();
        Assert.NotNull(workoutsResponse);
        Assert.Single(workoutsResponse.Workouts);
        Assert.True(workoutsResponse.Workouts.All(w => w.Description!.Contains("10")));
    }

    [Fact]
    public async Task GetAllWorkouts_WithSortOrder_ReturnsSortedResults()
    {
        // Arrange
        var user = DataSeedHelper.CreateUser();

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user);

            var workouts = new[]
            {
                new Workout("Workout 1", "1", user.Id),
                new Workout("Workout 2", "2", user.Id),
                new Workout("Workout 3", "3", user.Id)
        };

            foreach (var workout in workouts)
            {
                await dbContext.Workouts.AddAsync(workout);
            }

            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync("api/workouts?page=1&pageSize=10&sortOrder=desc");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var workoutsResponse = await response.Content.ReadFromJsonAsync<GetWorkoutsResponse>();

        Assert.NotNull(workoutsResponse);
        Assert.Equal(3, workoutsResponse.Workouts.Count());
        Assert.Equal("Workout 3", workoutsResponse.Workouts.First().Title);
    }

    [Fact]
    public async Task GetAllWorkouts_ReturnsOnlyCurrentUserWorkouts()
    {
        // Arrange
        var user1 = new User("TestUser1", "test1@example.com", "hashedpassword");
        var user2 = new User("TestUser2", "test2@example.com", "hashedpassword");

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            var workout1 = new Workout("User1 Workout", "U1", user1.Id);
            var workout2 = new Workout("User2 Workout", "U2", user2.Id);

            await dbContext.Workouts.AddAsync(workout1);
            await dbContext.Workouts.AddAsync(workout2);

            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user1);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync("api/workouts?page=1&pageSize=10");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var workoutsResponse = await response.Content.ReadFromJsonAsync<GetWorkoutsResponse>();

        Assert.NotNull(workoutsResponse);
        Assert.Single(workoutsResponse.Workouts);
        Assert.Equal("U1", workoutsResponse.Workouts.ElementAt(0).Description);
    }
}

