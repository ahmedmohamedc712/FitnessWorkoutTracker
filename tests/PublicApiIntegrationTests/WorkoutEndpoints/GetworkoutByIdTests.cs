using System.Net;
using System.Net.Http.Json;
using Application.Features.Workouts.GetById;
using Domain.Entities;
using PublicApiIntegrationTests.Extensions;
using PublicApiIntegrationTests.Helpers;

namespace PublicApiIntegrationTests.WorkoutEndpoints;

[Collection("Database Shared Collection")]
public class GetWorkoutByIdTests : IAsyncLifetime
{
    private const string TimeZoneHeader = "X-TimeZone";

    private readonly CustomWebApplicationFactory _factory;
    private HttpClient? _client = default;
    public GetWorkoutByIdTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }
    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync();
    }
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetWorkoutById_WithValidRequest_ReturnUserWorkout()
    {
        // Arrange
        var user1 = DataSeedHelper.CreateUser();
        var user2 = DataSeedHelper.CreateUser();

        var workout1 = new Workout("Workout 1", "U1", user1.Id);
        var workout2 = new Workout("Workout 2", "U2", user2.Id);

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.Workouts.AddAsync(workout1);
            await dbContext.Workouts.AddAsync(workout2);
            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user2);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync($"api/workouts/{workout2.Id}");

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        var workoutResponse = await response.Content.ReadFromJsonAsync<GetWorkoutByIdResponse>();

        Assert.NotNull(workoutResponse);
        Assert.Equal("Workout 2", workoutResponse.Title);
    }

    [Fact]
    public async Task GetWorkoutById_TryToAccessAnotherUserWorkout_ReturnsNotFound()
    {
        // Arrange
        var user1 = DataSeedHelper.CreateUser();
        var user2 = DataSeedHelper.CreateUser();

        var workout1 = new Workout("Workout 1", "U1", user1.Id);
        var workout2 = new Workout("Workout 2", "U2", user2.Id);

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user1);
            await dbContext.Users.AddAsync(user2);

            await dbContext.Workouts.AddAsync(workout1);
            await dbContext.Workouts.AddAsync(workout2);
            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user2);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync($"api/workouts/{workout1.Id}"); // we're trying to access user1 workout

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetWorkoutById_NotProvideTimezoneHeader_ReturnsBadRequest()
    {
        // Arrange
        var user1 = DataSeedHelper.CreateUser();

        var workout1 = new Workout("Workout 1", "U1", user1.Id);

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user1);

            await dbContext.Workouts.AddAsync(workout1);

            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user1);

        var response = await _client.GetAsync($"api/workouts/{workout1.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
