using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ScheduledWorkouts.GetById;

public class GetScheduledWorkoutByIdEndpointRequest
{
    [FromHeader(HeaderNames.TIME_ZONE_HEADER)]
    public string UserZone { get; set; } = null!;

}
