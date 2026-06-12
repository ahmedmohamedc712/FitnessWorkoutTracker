namespace Application.Features.Exercises.Create
{
    public interface ICreateExerciseUseCase
    {
        Task<CreateExerciseResponse> ExecuteAsync(Guid workoutId, CreateExerciseRequest req);
    }
}
