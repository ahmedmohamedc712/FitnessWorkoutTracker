namespace Application.Features.Exercises.GetAll
{
    public interface IGetExercisesUseCases
    {
        Task<GetExercisesResponse> ExecuteAsync(Guid workoutId, string userZone);
    }
}
