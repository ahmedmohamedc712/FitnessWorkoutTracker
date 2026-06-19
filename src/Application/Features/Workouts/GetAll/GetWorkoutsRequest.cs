namespace Application.Features.Workouts.GetAll;

public class GetWorkoutsRequest
{
    public string? SearchTerm { get; set; }
    public string? SortOrder { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
