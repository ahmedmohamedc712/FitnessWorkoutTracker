using Application.Features.Exercises.Create;
using FastEndpoints;

namespace PublicApi.Endpoints.Exercises.Create
{
    public class CreateExerciseEndpoint(ICreateExerciseUseCase createExerciseUseCase) : Endpoint<CreateExerciseRequest, CreateExerciseResponse>
    {
        public override void Configure()
        {
            Post("api/workouts/{workoutId}/exercises");
        }

        public override async Task HandleAsync(CreateExerciseRequest req, CancellationToken ct)
        {
            Guid workoutId = Route<Guid>("workoutId");

            var response = await createExerciseUseCase.ExecuteAsync(workoutId, req);

            await Send.OkAsync(response, cancellation: ct);
        }
    }
}
