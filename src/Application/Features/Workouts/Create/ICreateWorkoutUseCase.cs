namespace Application.Features.Workouts.Create
{
    public interface ICreateWorkoutUseCase
    {
        Task<CreateWorkoutResponse> ExecuteAsync(CreateWorkoutCommand command);
    }
}
