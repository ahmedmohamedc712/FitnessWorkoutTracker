using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.ScheduledWorkouts;

public class GetScheduledWorkoutsReadonlySpec : Specification<ScheduledWorkout>
{
    public GetScheduledWorkoutsReadonlySpec(
        Guid workoutId,
        Guid userId,
        string? sortOrder,
        int skip,
        int take
    )
    {
        Query
            .AsNoTracking()
            .Where(x => x.WorkoutId == workoutId && x.Workout!.UserId == userId);

        if (string.Equals(sortOrder, "asc"))
        {
            Query.OrderBy(x => x.SessionDate);
        }
        else
        {
            Query.OrderByDescending(x => x.SessionDate);
        }

        Query
            .Skip(skip)
            .Take(take);
    }
}
