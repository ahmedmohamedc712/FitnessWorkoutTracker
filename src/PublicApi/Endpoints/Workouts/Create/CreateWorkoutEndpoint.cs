using Application.Features.Workouts.Create;
using FastEndpoints;

namespace PublicApi.Endpoints.Workouts.Create
{
    public class CreateWorkoutEndpoint(ICreateWorkoutUseCase createWorkoutUseCase)
        : Endpoint<CreateWorkoutCommand, CreateWorkoutResponse>
    {
        public override void Configure()
        {
            Post("api/workouts");
        }

        public override async Task HandleAsync(CreateWorkoutCommand req, CancellationToken ct)
        {
            var response = await createWorkoutUseCase.ExecuteAsync(req);

            await Send.OkAsync(response, cancellation: ct);
        }
    }
}
