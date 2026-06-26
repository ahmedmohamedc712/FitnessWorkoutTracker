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
    }

    public override async Task HandleAsync(GetScheduledWorkoutsEndpointRequest req, CancellationToken ct)
    {
        var workoutId = Route<Guid>("workoutId");

        var query = new GetScheduledWorkoutsQuery(req.Page, req.PageSize, req.SortOrder);

        var response = await getScheduledWorkoutsUseCase.ExecuteAsync(query, workoutId, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }

}
