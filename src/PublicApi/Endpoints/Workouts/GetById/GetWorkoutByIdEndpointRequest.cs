using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Workouts.GetById;

public class GetWorkoutByIdEndpointRequest
{
    [FromHeader(HeaderNames.TIME_ZONE_HEADER)]
    public string UserZone { get; set; } = null!;
}
