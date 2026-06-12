using Domain.Entities;

namespace Application.Abstraction;

public interface IScheduledWorkoutRepository
{
    Task<ScheduledWorkout?> GetByIdWithWorkoutThenExercises(Guid scheduledWorkoutId);
    Task<ScheduledWorkout?> GetByIdWithExerciseProgressesThenWithExercise(Guid scheduledWorkoutId, Guid userId);
    Task<IEnumerable<ScheduledWorkout>> GetAllWithWorkout(Guid workoutId, Guid userId);
    Task SaveChangesAsync();
}
