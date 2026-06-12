namespace Application.Features.ExerciseProgresses.Update;

public interface IUpdateExerciseProgressStatusUseCase
{
    Task<UpdateExerciseProgressStatusResponse> ExecuteAsync(Guid exerciseProgressId, string userZone, UpdateExerciseProgressStatusRequest req);
}