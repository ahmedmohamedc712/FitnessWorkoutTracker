namespace Application.Features.Workouts.Create
{
    public interface ICreateWorkoutUseCase
    {
        Task<Guid> ExecuteAsync(CreateWorkoutCommand command);
    }
}
