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

        Description(b =>
        {
            b.WithSummary("Get exercise progress by ID.");
            b.WithDescription("Retrieve a single exercise progress record by its identifier.");

            b.Produces<GetExerciseProgressByIdResponse>(StatusCodes.Status200OK);
            b.Produces(StatusCodes.Status404NotFound);
            b.Produces(StatusCodes.Status401Unauthorized);

            b.WithTags(Constants.Tags.ExerciseProgressesTag);
        });
    }

    public override async Task HandleAsync(GetExerciseProgressByIdEndpointRequest req, CancellationToken ct)
    {
        var exerciseProgressId = Route<Guid>("id");

        var response = await getExerciseProgressByIdUseCase.ExecuteAsync(exerciseProgressId, req.UserZone);

        await Send.OkAsync(response, cancellation: ct);
    }

}
