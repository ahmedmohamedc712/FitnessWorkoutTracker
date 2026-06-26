using Application.Abstraction;
using Application.Exceptions;
using Application.Specifications.ExerciseProgresses;
using Domain.Entities;
using NodaTime.TimeZones;

namespace Application.Features.ExerciseProgresses.Start;

public class StartExerciseProgressUseCase(IRepository<ExerciseProgress> repository,
    ICurrentUserAccessor currentUserAccessor,
    IUtcLocalConverter utcLocalConverter,
    IAppLogger<StartExerciseProgressUseCase> logger) : IStartExerciseProgressUseCase
{
    public async Task<StartExerciseResponse> ExecuteAsync(Guid exerciseProgressId,
        StartExerciseRequest request,
        string userZone)
    {
        var userId = currentUserAccessor.GetId();

        var spec = new GetExerciseProgressByIdWithScheduledWorkoutSpec(exerciseProgressId, userId);

        var exerciseProgress = await repository.FirstOrDefaultAsync(spec);

        if (exerciseProgress is null)
        {
            logger.LogInformation("Exercise progress with ID `{ExerciseProgressId}` not found. UserId: {UserId}", exerciseProgressId, userId);
            throw new NotFoundException($"Exercise progress `{exerciseProgressId}` not found.");
        }

        exerciseProgress.Start(request.Sets, request.Reps);

        logger.LogInformation("Exercise progress started. ExerciseProgressId: {ExerciseProgressId}, Sets: {Sets}, Reps: {Reps}, UserId: {UserId}",
            exerciseProgressId, exerciseProgress.Sets, exerciseProgress.Reps, userId);

        var response = new StartExerciseResponse()
        {
            Id = exerciseProgressId,
            StartedAt = utcLocalConverter
                .ConvertUtcToLocal(exerciseProgress.StartedAt!.Value, userZone),

            Sets = exerciseProgress.Sets,
            Reps = exerciseProgress.Reps,
            Status = exerciseProgress.Status
        };

        await repository.SaveChangesAsync();

        return response;
    }
}
