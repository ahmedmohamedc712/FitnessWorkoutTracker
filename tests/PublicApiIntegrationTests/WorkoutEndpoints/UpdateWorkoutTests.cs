using System.Net;
using System.Net.Http.Json;
using Application.Features.Workouts.Update;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PublicApiIntegrationTests.Extensions;
using PublicApiIntegrationTests.Helpers;

namespace PublicApiIntegrationTests.WorkoutEndpoints;

[Collection("Database Shared Collection")]
public class UpdateWorkoutTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private HttpClient? _client;

    public UpdateWorkoutTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync();
    }
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdateWorkout_WithValidRequest_ReturnsNoContent()
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
        _client = _factory.CreateAuthenticatedClient(user1);

        var request = new UpdateWorkoutRequest()
        {
            Title = "Updated Workout 1",
            Description = "Updated description"
        };
        var response = await _client.PatchAsJsonAsync($"api/workouts/{workout1.Id}", request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var updatedWorkout = await dbContext.Workouts.FirstOrDefaultAsync(x => x.Id == workout1.Id);
        Assert.NotNull(updatedWorkout);
        Assert.Equal(request.Title, updatedWorkout.Title);
    }
    [Fact]
    public async Task UpdateWorkout_TryToUpdatedAnotherUserWorkout_ReturnsNotFound()
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
        _client = _factory.CreateAuthenticatedClient(user1);

        var request = new UpdateWorkoutRequest()
        {
            Title = "Updated Workout 1",
            Description = "Updated description"
        };
        var response = await _client.PatchAsJsonAsync($"api/workouts/{workout2.Id}", request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var updatedWorkout = await dbContext.Workouts.FirstOrDefaultAsync(x => x.Id == workout2.Id);
        Assert.NotNull(updatedWorkout);
        Assert.Equal("Workout 2", workout2.Title);
    }
}