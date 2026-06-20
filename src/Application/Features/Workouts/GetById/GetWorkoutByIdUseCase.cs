using Application.Abstraction;
using Application.Exceptions;
using Application.Specifications.Workouts;
using Domain.Entities;
using NodaTime.TimeZones;

namespace Application.Features.Workouts.GetById;

public class GetWorkoutByIdUseCase(IReadRepository<Workout> readRepository,
    ICurrentUserAccessor currentUserAccessor,
    IUtcLocalConverter utcLocalConverter,
    IAppLogger<GetWorkoutByIdUseCase> logger) : IGetWorkoutByIdUseCase
{
    public async Task<GetWorkoutByIdResponse> ExecuteAsync(Guid workoutId, string userZone)
    {
        if (string.IsNullOrWhiteSpace(userZone))
        {
            logger.LogDebug("Time zone header missing for retrieving workout.");
            throw new DateTimeZoneNotFoundException("");
        }

        var userId = currentUserAccessor.GetId();

        var spec = new GetWorkoutByIdReadonlySpec(workoutId, userId);
        var workout = await readRepository.FirstOrDefaultAsync(spec);

        if (workout is null)
        {
            logger.LogInformation("Workout not found.\nWorkoutId: {WorkoutId}",
                workoutId);

            throw new NotFoundException($"Workout with ID `{workoutId}` not found.");
        }

        return new GetWorkoutByIdResponse()
        {
            Id = workout.Id,
            Title = workout.Title,
            Description = workout.Description,
            CreatedAt = utcLocalConverter.ConvertUtcToLocal(workout.CreatedAt, userZone),
            ExercisesCount = workout.ExercisesCount,
        };
    }

}
