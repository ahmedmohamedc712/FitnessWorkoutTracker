using Application.Features.ScheduledWorkouts.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ScheduledWorkouts.GetAll;

public class GetScheduledWorkoutsEndpoint(IGetScheduledWorkoutsUseCase getScheduledWorkoutsUseCase)
    : Endpoint<GetScheduledWorkoutsEndpointRequest, GetScheduledWorkoutsResponse>
{
    public override void Configure()
    {
        Get("api/workouts/{workoutId}/scheduled-workouts");

        Description(b =>
        {
            b.WithSummary("Get scheduled workouts by workout ID.");
            b.WithDescription("Get scheduled workouts for the current user by workout ID.");

            b.Produces<GetScheduledWorkoutsResponse>(StatusCodes.Status200OK);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ScheduledWorkoutsTag);
        });
    }

    public override async Task HandleAsync(GetScheduledWorkoutsEndpointRequest req, CancellationToken ct)
    {
        var workoutId = Route<Guid>("workoutId");

        var query = new GetScheduledWorkoutsQuery(req.Page, req.PageSize, req.SortOrder);

        var response = await getScheduledWorkoutsUseCase.ExecuteAsync(query, workoutId, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }

}
