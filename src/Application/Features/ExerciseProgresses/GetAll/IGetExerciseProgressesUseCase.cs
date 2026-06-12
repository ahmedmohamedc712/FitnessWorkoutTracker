namespace Application.Features.ExerciseProgresses.GetAll;

public interface IGetExerciseProgressesUseCase
{
    Task<GetExerciseProgressesResponse> ExecuteAsync(Guid scheduledWorkoutId, string userZone);
}
