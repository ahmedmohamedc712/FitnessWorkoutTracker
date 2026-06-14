using Application.Abstraction;
using Application.Features.Workouts.Create;
using Domain.Entities;
using NodaTime.TimeZones;

namespace Application.Features.Workouts.GetAll
{
    public class GetWorkoutsUseCase(IWorkoutRepository workoutRepository,
        ICurrentUserAccessor currentUserAccessor,
        IUtcLocalConverter utcLocalConverter
    ) : IGetWorkoutsUseCase
    {
        public async Task<GetWorkoutsResponse> ExecuteAsync(string userZone)
        {
            if (string.IsNullOrWhiteSpace(userZone))
                throw new DateTimeZoneNotFoundException("");

            var userId = currentUserAccessor.GetId();

            var workouts = await workoutRepository.GetAllAsync(userId);

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
                Workouts = workoutDtos
            };
        }
    }
}
