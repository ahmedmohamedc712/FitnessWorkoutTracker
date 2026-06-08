using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstraction
{
    public interface IWorkoutRepository
    {
        Task<Workout?> GetWorkoutWithRelatedExercises(Guid workoutId, Guid userId);
        Task SaveChangesAsync();
    }
}
