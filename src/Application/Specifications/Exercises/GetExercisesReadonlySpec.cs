using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Exercises;

public class GetExercisesReadonlySpec : Specification<Exercise>
{
    public GetExercisesReadonlySpec(
        Guid workoutId,
        Guid userId,
        string? searchTerm,
        string? sortOrder,
        int skip,
        int take
    )
    {
        Query
            .AsNoTracking()
            .Where(x => x.WorkoutId == workoutId && x.Workout!.UserId == userId);

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
