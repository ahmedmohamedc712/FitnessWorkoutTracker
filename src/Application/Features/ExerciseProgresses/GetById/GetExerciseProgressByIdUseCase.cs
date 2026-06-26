using Application.Abstraction;
using Application.Exceptions;
using Application.Specifications.ExerciseProgresses;
using Domain.Entities;
using NodaTime.TimeZones;

namespace Application.Features.ExerciseProgresses.GetById;

public class GetExerciseProgressByIdUseCase(IReadRepository<ExerciseProgress> readRepository,
    ICurrentUserAccessor currentUserAccessor,
    IUtcLocalConverter utcLocalConverter,
    IAppLogger<GetExerciseProgressByIdUseCase> logger) : IGetExerciseProgressByIdUseCase
{
    public async Task<GetExerciseProgressByIdResponse> ExecuteAsync(Guid exerciseProgressId, string userZone)
    {
        var userId = currentUserAccessor.GetId();

        var spec = new GetExerciseProgressByIdWithExerciseAndNotesReadonlySpec(exerciseProgressId, userId);

        var exerciseProgress = await readRepository.FirstOrDefaultAsync(spec);

        if (exerciseProgress is null)
        {
            logger.LogInformation("Exercise progress with ID `{ExerciseProgressId}` not found. UserId: {UserId}", exerciseProgressId, userId);
            throw new NotFoundException($"Exercise progress with ID `{exerciseProgressId}` not found.");
        }

        var response = new GetExerciseProgressByIdResponse()
        {
            Id = exerciseProgress.Id,
            Title = exerciseProgress.Exercise!.Title,
            Description = exerciseProgress.Exercise!.Description,
            Sets = exerciseProgress.Sets,
            Reps = exerciseProgress.Reps,
            Status = exerciseProgress.Status,
            StartedAt = exerciseProgress.StartedAt == null
                ? null
                : utcLocalConverter.ConvertUtcToLocal(exerciseProgress.StartedAt.Value, userZone),

            CompletedAt = exerciseProgress.CompletedAt == null
                ? null
                : utcLocalConverter.ConvertUtcToLocal(exerciseProgress.CompletedAt.Value, userZone),
                
            Notes = exerciseProgress.Notes.Select(x => new NoteDto()
            {
                Id = x.Id,
                Content = x.Content
            })
        };
        logger.LogDebug("Retrieved exercise progress details. ExerciseProgressId: {ExerciseProgressId}, NotesCount: {NotesCount}, UserId: {UserId}",
            exerciseProgressId, exerciseProgress.Notes.Count, userId);

        return response;
    }
}
