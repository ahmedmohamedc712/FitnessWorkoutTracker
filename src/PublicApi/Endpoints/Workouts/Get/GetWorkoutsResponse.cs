using Application.Features.Workouts.GetAll;

namespace PublicApi.Endpoints.Workouts.Get
{
    public class GetWorkoutsResponse
    {
        public List<WorkoutDto> WorkoutDtos { get; set; } = [];
    }
}
