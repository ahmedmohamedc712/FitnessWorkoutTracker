using Application.Features.Workouts.GetAll;
using FastEndpoints;
using PublicApi.Constants;

namespace PublicApi.Endpoints.Workouts.GetAll
{
    public class GetWorkoutsEndpoint(IGetWorkoutsUseCase getWorkoutsUseCase) 
        : Endpoint<GetWorkoutsEndpointRequest, GetWorkoutsResponse>
    {
        public override void Configure()
        {
            Get("api/workouts");

            Description(b =>
            {
                b.WithSummary("Get all workouts.");
                b.WithDescription("Get all workouts for the current user.");

                b.Produces<GetWorkoutsResponse>(StatusCodes.Status200OK);
                b.Produces(StatusCodes.Status401Unauthorized);

                b.WithTags(Constants.Tags.WorkoutsTag);
            });
        }

        public override async Task HandleAsync(GetWorkoutsEndpointRequest req, CancellationToken ct)
        {
            var query = new GetWorkoutsQuery(req.Page, req.PageSize, req.SearchTerm, req.SortOrder);

            var response = await getWorkoutsUseCase.ExecuteAsync(query, req.UserZone);

            await Send.OkAsync(response, cancellation: ct);
        }
    }
}
