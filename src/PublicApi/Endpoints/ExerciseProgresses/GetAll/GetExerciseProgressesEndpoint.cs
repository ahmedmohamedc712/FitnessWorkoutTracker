using Application.Features.ExerciseProgresses.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.GetAll;

public class GetExerciseProgressesEndpoint(IGetExerciseProgressesUseCase getExerciseProgressesUseCase)
    : Endpoint<GetExerciseProgressesEndpointRequest, GetExerciseProgressesResponse>
{
    public override void Configure()
    {
        Get("api/scheduled-workouts/{scheduledWorkoutId}/exercise-progresses");
    }

    public override async Task HandleAsync(GetExerciseProgressesEndpointRequest req, CancellationToken ct)
    {
        var scheduledWorkoutId = Route<Guid>("scheduledWorkoutId");

        var response = await getExerciseProgressesUseCase
            .ExecuteAsync(scheduledWorkoutId, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }
}
