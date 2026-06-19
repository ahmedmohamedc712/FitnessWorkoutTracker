using Application.Abstraction;
using Application.Exceptions;
using Application.Specifications.Exercises;
using Application.Specifications.Workouts;
using Ardalis.Specification;
using Domain.Entities;
using NodaTime.TimeZones;

namespace Application.Features.Exercises.GetAll
{
    public class GetExercisesUseCase(
        IReadRepository<Workout> workoutRepository,
        IReadRepository<Exercise> exerciseRepository,
        ICurrentUserAccessor currentUserAccessor,
        IUtcLocalConverter utcLocalConverter,
        IAppLogger<GetExercisesUseCase> logger) : IGetExercisesUseCase
    {
        public async Task<GetExercisesResponse> ExecuteAsync(GetExercisesRequest req, Guid workoutId, string userZone)
        {
            if (string.IsNullOrWhiteSpace(userZone))
            {
                logger.LogDebug("Timezone information missing for retrieving exercising. WorkoutId: {WorkoutId}", workoutId);
                throw new DateTimeZoneNotFoundException("");
            }

            var userId = currentUserAccessor.GetId();

            var workoutSpec = new WorkoutExistsReadonlySpec(workoutId, userId);
            var workoutExists = await workoutRepository.AnyAsync(workoutSpec);

            if (!workoutExists)
            {
                logger.LogInformation("Workout not found for retrieving exercises.\nWorkoutId: {WorkoutId}",
                    workoutId);

                throw new NotFoundException($"Workout with ID `{workoutId} not found.`");
            }

            var exercisesSpec = new GetExercisesReadonlySpec(
                workoutId,
                userId,
                req.SearchTerm?.Trim().ToLower(),
                req.SortOrder?.Trim().ToLower(),
                skip: (req.Page - 1) * req.PageSize,
                take: req.PageSize
            );

            var  exercises = await exerciseRepository.ListAsync(exercisesSpec);

            var exerciseDtos = exercises.Select(x => new ExerciseDto()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = utcLocalConverter.ConvertUtcToLocal(x.CreatedAt, userZone)
            });

            return new GetExercisesResponse()
            {
                ExerciseDtos = exerciseDtos
            };
        }
    }
}
