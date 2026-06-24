using System.Net;
using System.Net.Http.Json;
using Application.Features.Workouts.Create;
using PublicApiIntegrationTests.Extensions;
using PublicApiIntegrationTests.Helpers;

namespace PublicApiIntegrationTests.WorkoutEndpoints;

[Collection("Database Shared Collection")]
public class CreateWorkoutTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private HttpClient? _client;

    public CreateWorkoutTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync(); 
    }
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task CreateWorkout_WithValidRequest_ReturnsCreated()
    {
        // Arrange
        var user = DataSeedHelper.CreateUser();

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user);
        var command = new CreateWorkoutCommand()
        {
            Title = "Workout 1",
            Description = "U1" 
        };
        
        var response = await _client.PostAsJsonAsync("api/workouts", command);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createResponse = await response.Content.ReadFromJsonAsync<CreateWorkoutResponse>();
        
        Assert.NotNull(createResponse);
        Assert.Equal(command.Title, createResponse.Title);
        Assert.Equal(command.Description, createResponse.Description);
    }

    [Fact]
    public async Task CreateWorkout_NotProvideTitle_ReturnsBadRequest()
    {
        // Arrange
        var user = DataSeedHelper.CreateUser();

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user);
        var command = new CreateWorkoutCommand()
        {
            Description = "U1" 
        };
        
        var response = await _client.PostAsJsonAsync("api/workouts", command);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
