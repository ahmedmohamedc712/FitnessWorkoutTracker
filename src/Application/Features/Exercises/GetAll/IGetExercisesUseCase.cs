namespace Application.Features.Exercises.GetAll
{
    public interface IGetExercisesUseCase
    {
        Task<GetExercisesResponse> ExecuteAsync(GetExercisesRequest req, Guid workoutId, string userZone);
    }
}
