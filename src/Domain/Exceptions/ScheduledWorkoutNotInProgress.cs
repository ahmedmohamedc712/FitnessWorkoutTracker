namespace Domain.Exceptions;

public class ScheduledWorkoutNotInProgress : Exception
{
    public ScheduledWorkoutNotInProgress(Guid scheduledWorkoutId) : base ($"Scheduled workout {scheduledWorkoutId} not in in-progress status.")
    {
        
    }
}
