namespace Application.Features.Exercises.GetAll;

public class GetExercisesRequest
{
    public string? SearchTerm { get; set; }
    public string? SortOrder { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
