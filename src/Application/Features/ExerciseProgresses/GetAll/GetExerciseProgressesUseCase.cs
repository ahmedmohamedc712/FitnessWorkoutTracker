using Application.Abstraction;
using Application.Exceptions;
using Application.Specifications.ExerciseProgresses;
using Application.Specifications.ScheduledWorkouts;
using Domain.Entities;
using Domain.Exceptions;
using NodaTime.TimeZones;

namespace Application.Features.ExerciseProgresses.GetAll;

public class GetExerciseProgressesUseCase(IReadRepository<ScheduledWorkout> scheduledWorkoutRepository,
    IReadRepository<ExerciseProgress> exerciseProgressRepository,
    ICurrentUserAccessor currentUserAccessor,
    IUtcLocalConverter utcLocalConverter,
    IAppLogger<GetExerciseProgressesUseCase> logger) : IGetExerciseProgressesUseCase
{
    public async Task<GetExerciseProgressesResponse> ExecuteAsync(Guid scheduledWorkoutId, string userZone)
    {
        var userId = currentUserAccessor.GetId();

        var scheduledSpec = new ScheduledWorkoutExistsReadonlySpec(scheduledWorkoutId, userId);
        var scheduledWorkout = await scheduledWorkoutRepository.FirstOrDefaultAsync(scheduledSpec);

        if (scheduledWorkout is null)
        {
            logger.LogInformation("Scheduled workout with ID `{ScheduledWorkoutId}` not found. UserId: {UserId}",
                scheduledWorkoutId,
                userId);

            throw new NotFoundException($"Scheduled workout with ID `{scheduledWorkoutId}` not found.");
        }

        if (scheduledWorkout.Status == WorkoutStatus.Pending)
        {
            throw new ScheduledWorkoutPendingException("Scheduled workout is pending and does not have exercise progresses.");
        }
                
        var exerciseSpec = new GetExerciseProgressesWithExerciseReadonlySpec(scheduledWorkoutId, userId);

        var exerciseProgresses = await exerciseProgressRepository.ListAsync(exerciseSpec);

        var exerciseProgressDtos = exerciseProgresses.Select(exerciseProgress => new ExerciseProgressDto()
        {
            Id = exerciseProgress.Id,
            Title = exerciseProgress.Exercise!.Title,
            Description = exerciseProgress.Exercise.Description,
            Sets = exerciseProgress.Sets,
            Reps = exerciseProgress.Reps,
            Status = exerciseProgress.Status,
            StartedAt = exerciseProgress.StartedAt == null
                    ? null
                    : utcLocalConverter.ConvertUtcToLocal(exerciseProgress.StartedAt.Value, userZone),
            CompletedAt = exerciseProgress.CompletedAt == null
                    ? null
                    : utcLocalConverter.ConvertUtcToLocal(exerciseProgress.CompletedAt.Value, userZone)

        }).ToList();

        logger.LogInformation("Retrieved {ExerciseProgressCount} exercise progresses for scheduled workout {ScheduledWorkoutId}. UserId: {UserId}",
            exerciseProgressDtos.Count, scheduledWorkoutId, userId);

        return new GetExerciseProgressesResponse()
        {
            ExerciseProgressDtos = exerciseProgressDtos
        };
    }
}
