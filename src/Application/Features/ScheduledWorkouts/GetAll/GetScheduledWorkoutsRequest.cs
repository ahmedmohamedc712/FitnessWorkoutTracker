namespace Application.Features.ScheduledWorkouts.GetAll;

public class GetScheduledWorkoutsRequest
{
    public string? SortOrder { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

}