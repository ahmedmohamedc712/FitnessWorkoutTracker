using Application.Abstraction;
using Application.Features.Workouts.Create;
using Domain.Entities;

namespace Application.Features.Workouts.GetAll
{
    public class GetWorkoutsUseCase(IWorkoutRepository workoutRepository,
        ICurrentUserAccessor currentUserAccessor,
        IUtcLocalConverter utcLocalConverter
    )  
    {
        public async Task<GetWorkoutsResponse> ExecuteAsync(string userZone)
        {
            var userId = currentUserAccessor.GetId();

            var workouts =  await workoutRepository.GetAllAsync(userId);

            var workoutDtos = workouts.Select(x => new WorkoutDto()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ExercisesCount = x.ExercisesCount,
                CreatedAt = utcLocalConverter.ConvertUtcToLocal(x.CreatedAt, userZone)
            });

            return new GetWorkoutsResponse()
            {
                Workouts = [.. workoutDtos]
            };
        }
    }
}
