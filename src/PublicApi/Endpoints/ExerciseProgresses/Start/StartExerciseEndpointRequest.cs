using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.ExerciseProgresses.Start;

public class StartExerciseEndpointRequest
{
    [FromHeader(HeaderNames.TIME_ZONE_HEADER)]
    public string UserZone { get; set; } = null!;
    public int Sets { get; set; }
    public int Reps { get; set; }
}
