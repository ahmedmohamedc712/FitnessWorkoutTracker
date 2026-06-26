using Application.Features.Exercises.Create;
using FastEndpoints;

namespace PublicApi.Endpoints.Exercises.Create
{
    public class CreateExerciseEndpoint(ICreateExerciseUseCase createExerciseUseCase)
        : Endpoint<CreateExerciseRequest, CreateExerciseResponse>
    {
        public override void Configure()
        {
            Post("api/workouts/{workoutId}/exercises");

            Description(b =>
            {
                b.WithSummary("Create a new exercise.");
                b.WithDescription("Create a new exercise for a specific workout for the current user.");

                b.Produces<CreateExerciseResponse>(StatusCodes.Status200OK);
                b.Produces(StatusCodes.Status401Unauthorized);
            });
        }

        public override async Task HandleAsync(CreateExerciseRequest req, CancellationToken ct)
        {
            Guid workoutId = Route<Guid>("workoutId");

            var response = await createExerciseUseCase.ExecuteAsync(workoutId, req);

            await Send.OkAsync(response, cancellation: ct);
        }
    }
}
