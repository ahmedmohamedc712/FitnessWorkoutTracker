using Application.Features.Exercises.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Exercises.GetAll
{
    public class GetExercisesEndpoint(IGetExercisesUseCase getExercisesUseCases)
        : Endpoint<GetExercisesEndpointRequest, GetExercisesResponse>
    {
        public override void Configure()
        {
            Get("api/workouts/{workoutId}/exercises");

            Description(b =>
            {
                b.WithSummary("Get exercises for a workout.");
                b.WithDescription("Retrieve exercises for the specified workout belonging to the current user.");

                b.Produces<GetExercisesResponse>(StatusCodes.Status200OK);
                b.Produces(StatusCodes.Status404NotFound);
                b.Produces(StatusCodes.Status401Unauthorized);

                b.WithTags(Constants.Tags.ExercisesTag);
            });
        }

        public override async Task HandleAsync(GetExercisesEndpointRequest req, CancellationToken ct)
        {
            var workoutId = Route<Guid>("workoutId");

            var query = new GetExercisesQuery(req.Page, req.PageSize, req.SearchTerm, req.SortOrder);

            var response = await getExercisesUseCases.ExecuteAsync(query, workoutId, req.UserZone);

            await Send.OkAsync(response, cancellation: ct);
        }
    }
}
