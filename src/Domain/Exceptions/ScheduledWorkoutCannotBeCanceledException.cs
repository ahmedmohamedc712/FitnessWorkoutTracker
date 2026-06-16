namespace Domain.Exceptions;

public class ScheduledWorkoutCannotBeCanceledException : DomainException
{
    public ScheduledWorkoutCannotBeCanceledException(string message) : base(message)
    {
        
    }
}
