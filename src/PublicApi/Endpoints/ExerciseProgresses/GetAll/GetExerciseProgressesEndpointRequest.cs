using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.GetAll;

public class GetExerciseProgressesEndpointRequest
{
    [FromHeader(HeaderNames.TIME_ZONE_HEADER)]
    public string UserZone { get; set; } = null!;

}
