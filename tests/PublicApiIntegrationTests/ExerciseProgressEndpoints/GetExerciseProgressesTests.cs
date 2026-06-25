using System.Net;
using System.Net.Http.Json;
using Application.Features.ExerciseProgresses.GetAll;
using Domain.Entities;
using NodaTime;
using PublicApiIntegrationTests.Extensions;
using PublicApiIntegrationTests.Helpers;

namespace PublicApiIntegrationTests.ExerciseProgressEndpoints;

[Collection("Database Shared Collection")]
public class GetExerciseProgressesTests : IAsyncLifetime
{
    private const string TimeZoneHeader = "X-TimeZone";
    private readonly CustomWebApplicationFactory _factory;
    private HttpClient? _client;

    public GetExerciseProgressesTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        await _factory.ResetDatabaseAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetExerciseProgresses_WithValidRequest_ReturnsOk()
    {
        // Arrange
        var user = DataSeedHelper.CreateUser();

        var workout = new Workout("Workout 1", "U1", user.Id);

        workout.AddExercise(new Exercise("Exercise 1", null, workout.Id));
        workout.AddExercise(new Exercise("Exercise 2", null, workout.Id));
        workout.AddExercise(new Exercise("Exercise 3", null, workout.Id));
        workout.AddExercise(new Exercise("Exercise 4", null, workout.Id));

        var scheduledWorkout = ScheduledWorkout.Schedule(workout,
            SystemClock.Instance.GetCurrentInstant().Plus(Duration.FromDays(1)));

        scheduledWorkout.Start();

        workout.AddScheduledWorkout(scheduledWorkout);

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.Workouts.AddAsync(workout);
            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync($"api/scheduled-workouts/{scheduledWorkout.Id}/exercise-progresses");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var getExerciseProgressesResponse = await response.Content.ReadFromJsonAsync<GetExerciseProgressesResponse>();
        Assert.NotNull(getExerciseProgressesResponse);
        Assert.NotEmpty(getExerciseProgressesResponse.ExerciseProgressDtos);
    }

    [Fact]
    public async Task GetExerciseProgresses_TryToGetFromPendingScheduledWorkout_ReturnsBadRequest()
    {
        // Arrange
        var user = DataSeedHelper.CreateUser();

        var workout = new Workout("Workout 1", "U1", user.Id);

        workout.AddExercise(new Exercise("Exercise 1", null, workout.Id));
        workout.AddExercise(new Exercise("Exercise 2", null, workout.Id));
        workout.AddExercise(new Exercise("Exercise 3", null, workout.Id));
        workout.AddExercise(new Exercise("Exercise 4", null, workout.Id));

        var scheduledWorkout = ScheduledWorkout.Schedule(workout,
            SystemClock.Instance.GetCurrentInstant().Plus(Duration.FromDays(1)));

        workout.AddScheduledWorkout(scheduledWorkout);

        await _factory.SeedAsync(async dbContext =>
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.Workouts.AddAsync(workout);
            await dbContext.SaveChangesAsync();
        });

        // Act
        _client = _factory.CreateAuthenticatedClient(user);
        _client.DefaultRequestHeaders.Add(TimeZoneHeader, "UTC");

        var response = await _client.GetAsync($"api/scheduled-workouts/{scheduledWorkout.Id}/exercise-progresses");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
