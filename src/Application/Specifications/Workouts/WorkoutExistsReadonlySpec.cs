using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Workouts;

public class WorkoutExistsReadonlySpec : Specification<Workout>
{
    public WorkoutExistsReadonlySpec(Guid workoutId, Guid userId)
    {
        Query
            .AsNoTracking()
            .Where(x => x.Id == workoutId && x.UserId == userId);
    }
}
