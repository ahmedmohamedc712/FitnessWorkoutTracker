using System.Linq.Expressions;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Workouts;

public class GetAllWorkoutsReadonlySpec : Specification<Workout>
{
    public GetAllWorkoutsReadonlySpec(Guid userId,
        string? searchTerm,
        string? sortOrder,
        int skip,
        int take
    )
    {
        Query
            .AsNoTracking()
            .Where(x => x.UserId == userId);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            Query.Where(x => x.Title.Contains(searchTerm));
        }

        if (string.Equals(sortOrder, "asc"))
        {
            Query.OrderBy(x => x.CreatedAt);
        }
        else
        {
            Query.OrderByDescending(x => x.CreatedAt);
        }

        Query
            .Skip(skip)
            .Take(take);
    }
}
