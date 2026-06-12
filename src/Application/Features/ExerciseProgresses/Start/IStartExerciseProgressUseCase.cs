namespace Application.Features.ExerciseProgresses.Start;

public interface IStartExerciseProgressUseCase
{
    Task<StartExerciseResponse> ExecuteAsync(Guid exerciseProgressId, StartExerciseRequest request, string userZone);
}
