using Application.Features.Workouts.Create;
using FastEndpoints;
using PublicApi.Endpoints.Workouts.GetById;

namespace PublicApi.Endpoints.Workouts.Create
{
    public class CreateWorkoutEndpoint(ICreateWorkoutUseCase createWorkoutUseCase)
        : Endpoint<CreateWorkoutCommand>
    {
        public override void Configure()
        {
            Post("api/workouts");

            Description(b =>
            {
                b.WithSummary("Create a new workout.");
                b.WithDescription("Create a new workout for the current user, and provide a location header for the new record.");

                b.Produces(StatusCodes.Status201Created);
                b.Produces(StatusCodes.Status401Unauthorized);

                b.WithTags(Constants.Tags.WorkoutsTag);
            });
        }

        public override async Task HandleAsync(CreateWorkoutCommand req, CancellationToken ct)
        {
            var workoutId = await createWorkoutUseCase.ExecuteAsync(req);

            await Send.CreatedAtAsync<GetWorkoutByIdEndpoint>(
                new { id = workoutId },
                generateAbsoluteUrl: true,
                cancellation: ct
            );
        }
    }
}
