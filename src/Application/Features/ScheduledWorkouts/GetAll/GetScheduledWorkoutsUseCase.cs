using Application.Abstraction;
using Application.Features.Workouts.Create;
using NodaTime.TimeZones;

namespace Application.Features.ScheduledWorkouts.GetAll;

public class GetScheduledWorkoutsUseCase(IScheduledWorkoutRepository scheduledWorkoutRepository,
    ICurrentUserAccessor currentUserAccessor,
    IUtcLocalConverter utcLocalConverter) : IGetScheduledWorkoutsUseCase
{
    public async Task<GetScheduledExercisesResponse> ExecuteAsync(Guid workoutId, string userZone)
    {
        if (string.IsNullOrWhiteSpace(userZone))
            throw new DateTimeZoneNotFoundException("");

        var userId = currentUserAccessor.GetId();

        var scheduledWorkouts = await scheduledWorkoutRepository.GetAllWithWorkout(workoutId, userId);

        var scheduledWorkoutDtos = new List<ScheduledWorkoutDto>();
        foreach (var scheduledWorkout in scheduledWorkouts)
        {
            var scheduledWorkoutDto = new ScheduledWorkoutDto()
            {
                Id = scheduledWorkout.Id,
                Title = scheduledWorkout.Workout!.Title,
                Description = scheduledWorkout.Workout!.Description,
                SessionDate = utcLocalConverter.ConvertUtcToLocal(scheduledWorkout.SessionDate, userZone),
                Status = scheduledWorkout.Status
            };

            if (scheduledWorkout.StartedAt is not null)
                scheduledWorkoutDto.StartedAt = utcLocalConverter
                    .ConvertUtcToLocal(scheduledWorkout.StartedAt.GetValueOrDefault(), userZone);

            if (scheduledWorkout.CompletedAt is not null)
                scheduledWorkoutDto.CompletedAt = utcLocalConverter
                    .ConvertUtcToLocal(scheduledWorkout.CompletedAt.GetValueOrDefault(), userZone);

            scheduledWorkoutDtos.Add(scheduledWorkoutDto);
        }

        return new GetScheduledExercisesResponse()
        {
            ScheduledWorkoutDtos = scheduledWorkoutDtos
        };
    }
}
