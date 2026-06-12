namespace Application.Features.Exercises.Get
{
    public interface IGetExercisesUseCases
    {
        Task<GetExercisesResponse> ExecuteAsync(Guid workoutId, string userZone);
    }
}
