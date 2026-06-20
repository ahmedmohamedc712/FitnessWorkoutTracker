using System.Text.RegularExpressions;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Workouts;

public class GetWorkoutByIdReadonlySpec : Specification<Workout>
{
    public GetWorkoutByIdReadonlySpec(Guid workoutId, Guid userId)
    {
        Query
            .AsNoTracking()
            .Where(x => x.Id == workoutId && x.UserId == userId);
    }
}
