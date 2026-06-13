using Application.Abstraction;
using Application.Exceptions;
using Application.Features.ScheduledWorkouts.GetAll;
using Application.Features.Workouts.Create;
using NodaTime.TimeZones;

namespace Application.Features.ScheduledWorkouts.GetById;

public class GetScheduledWorkoutByIdUseCase(IScheduledWorkoutRepository scheduledWorkoutRepository,
    ICurrentUserAccessor currentUserAccessor,
    IUtcLocalConverter utcLocalConverter) : IGetScheduledWorkoutByIdUseCase
{
    public async Task<ScheduledWorkoutDto> ExecuteAsync(Guid scheduledWorkoutId, string userZone)
    {
        if (string.IsNullOrWhiteSpace(userZone))
            throw new DateTimeZoneNotFoundException("");

        var userId = currentUserAccessor.GetId();

        var scheduledWorkout = await scheduledWorkoutRepository.GetByIdWithWorkout(scheduledWorkoutId, userId);
        if (scheduledWorkout is null)
            throw new NotFoundException($"Scheduled workout with ID `{scheduledWorkoutId}` not found.");


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

        return scheduledWorkoutDto;
    }
}
