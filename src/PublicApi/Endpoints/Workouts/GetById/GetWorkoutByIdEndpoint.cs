using Application.Features.Workouts.GetById;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Workouts.GetById;

public class GetWorkoutByIdEndpoint(IGetWorkoutByIdUseCase getWorkoutByIdUseCase)
    : Endpoint<GetWorkoutByIdEndpointRequest, GetWorkoutByIdResponse>
{
    public override void Configure()
    {
        Get("api/workouts/{id}");
    }

    public override async Task HandleAsync(GetWorkoutByIdEndpointRequest req, CancellationToken ct)
    {
        var workoutId = Route<Guid>("id");

        var response = await getWorkoutByIdUseCase.ExecuteAsync(workoutId, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }
}
