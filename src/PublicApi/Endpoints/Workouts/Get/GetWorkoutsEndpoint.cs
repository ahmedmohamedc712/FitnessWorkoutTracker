using Application.Features.Workouts.GetAll;
using FastEndpoints;

namespace PublicApi.Endpoints.Workouts.Get
{
    public class GetWorkoutsEndpoint(GetWorkoutsUseCase getWorkoutsUseCase, 
        AutoMapper.IMapper mapper) : EndpointWithoutRequest<GetWorkoutsResponse>
    {
        public override void Configure()
        {
            Get("api/workouts");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await getWorkoutsUseCase.ExecuteAsync();

            var workoutDtos = result.Workouts.Select(x => mapper.Map<WorkoutDto>(x));

            var response = new GetWorkoutsResponse()
            {
                WorkoutDtos = [.. workoutDtos]
            };

            await SendAsync(response, cancellation: ct);
        }
    }
}
