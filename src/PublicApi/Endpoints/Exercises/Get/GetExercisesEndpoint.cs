using Application.Features.Exercises.Get;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Exercises.Get
{
    public class GetExercisesEndpoint(GetExercisesUseCases getExercisesUseCases)
        : EndpointWithoutRequest<GetExercisesResponse>
    {
        public override void Configure()
        {
            Get("api/workouts/{workoutId}/exercises");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userZone = HttpContext.Request.Headers[HeaderNames.TIME_ZONE_HEADER].ToString();

            var workoutId = Route<Guid>("workoutId");

            var response = await getExercisesUseCases.Execute(workoutId, userZone);

            await SendAsync(response, cancellation: ct);
        }
    }
}
