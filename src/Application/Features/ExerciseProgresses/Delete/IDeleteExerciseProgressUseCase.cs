namespace Application.Features.ExerciseProgresses.Delete;

public interface IDeleteExerciseProgressUseCase
{
    Task ExecuteAsync(Guid exerciseProgressId);
}
