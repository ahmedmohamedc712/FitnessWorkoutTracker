using Application.Abstraction;
using Application.Features.Workouts.Create;
using Domain.Entities;

namespace Application.Features.Workouts.GetAll
{
    public class GetWorkoutsUseCase(IRepository<Workout> workoutRepository,
        ICurrentUserAccessor currentUserAccessor
    )  
    {
        public async Task<GetWorkoutsResult> ExecuteAsync()
        {
            var userId = currentUserAccessor.GetId();

            var workouts =  await workoutRepository.WhereAsync(x => x.UserId == userId);

            return new GetWorkoutsResult()
            {
                Workouts = [.. workouts]
            };
        }
    }
}
