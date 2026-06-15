using Application.Abstraction;
using Application.Exceptions;
using Application.Features.Workouts.Create;
using NodaTime.TimeZones;

namespace Application.Features.ScheduledWorkouts.Reschedule;

public class RescheduleWorkoutUseCase(IScheduledWorkoutRepository scheduledWorkoutRepository,
    ICurrentUserAccessor currentUserAccessor,
    IUtcLocalConverter utcLocalConverter) : IRescheduleWorkoutUseCase
{
    public async Task ExecuteAsync(Guid scheduledWorkoutId, string userZone, DateTime sessionDate)
    {
        if (string.IsNullOrWhiteSpace(userZone))
            throw new DateTimeZoneNotFoundException("");

        var userId = currentUserAccessor.GetId();

        var scheduledWorkout = await scheduledWorkoutRepository.GetByIdAsync(scheduledWorkoutId, userId);

        if (scheduledWorkout is null)
            throw new NotFoundException($"Scheduled workout with ID `{scheduledWorkoutId}` not found.");

        var sessionInstant = utcLocalConverter.ConvertLocalToUtc(sessionDate, userZone);
        scheduledWorkout.Reschedule(sessionInstant);

        await scheduledWorkoutRepository.SaveChangesAsync();
    }
}
