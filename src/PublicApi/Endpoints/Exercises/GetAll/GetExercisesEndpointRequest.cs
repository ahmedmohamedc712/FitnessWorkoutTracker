using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Exercises.GetAll
{
    public class GetExercisesEndpointRequest
    {
        [FromHeader(HeaderNames.TIME_ZONE_HEADER)]
        public string UserZone { get; set; } = null!;
        [QueryParam]
        public string? SearchTerm { get; set; }
        [QueryParam]
        public string? SortOrder { get; set; }
        [QueryParam]
        public int Page { get; set; }
        [QueryParam]
        public int PageSize { get; set; }
    }
}
