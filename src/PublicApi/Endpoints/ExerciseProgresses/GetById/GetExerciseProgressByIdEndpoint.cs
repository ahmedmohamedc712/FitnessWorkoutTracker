using Application.Features.ExerciseProgresses.GetById;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.GetById;

public class GetExerciseProgressByIdEndpoint(IGetExerciseProgressByIdUseCase getExerciseProgressByIdUseCase)
    : Endpoint<GetExerciseProgressByIdEndpointRequest, GetExerciseProgressByIdResponse>
{
    public override void Configure()
    {
        Get("api/exercise-progresses/{id}");
    }

    public override async Task HandleAsync(GetExerciseProgressByIdEndpointRequest req, CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("id");

        var response = await getExerciseProgressByIdUseCase.ExecuteAsync(exerciseProgressId, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }

}
