using System.Net;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PublicApiIntegrationTests.Extensions;
using PublicApiIntegrationTests.Helpers;

namespace PublicApiIntegrationTests.WorkoutEndpoints;

[Collection("Database Shared Collection")]
public class DeleteWorkoutTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private HttpClient? _client;

    public DeleteWorkoutTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync();
    }
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task DeleteWorkout_WithValidRequest_ReturnsNoContent()
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

        var response = await _client.DeleteAsync($"api/workouts/{workout1.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var workoutExists = await dbContext.Workouts.AnyAsync(x => x.Id == workout1.Id);
        Assert.False(workoutExists);
    }
    
    [Fact]
    public async Task DeleteWorkout_TryToDeleteAnotherUserWorkout_ReturnsNotFound()
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

        var response = await _client.DeleteAsync($"api/workouts/{workout2.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var workoutExists = await dbContext.Workouts.AnyAsync(x => x.Id == workout1.Id);
        Assert.True(workoutExists);
    }
}