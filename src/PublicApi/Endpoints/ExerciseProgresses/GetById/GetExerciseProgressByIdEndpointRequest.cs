using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.GetById;

public class GetExerciseProgressByIdEndpointRequest
{
    [FromHeader(HeaderNames.TIME_ZONE_HEADER)]
    public string UserZone { get; set; } = null!;

}
