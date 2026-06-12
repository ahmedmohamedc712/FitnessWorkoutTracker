namespace Application.Features.ExerciseProgresses.GetById;

public interface IGetExerciseProgressByIdUseCase
{
    Task<GetExerciseProgressByIdResponse> ExecuteAsync(Guid exerciseProgressId, string userZone);
}
