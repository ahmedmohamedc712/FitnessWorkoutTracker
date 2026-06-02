namespace Domain.Exceptions;

public class NegativeNumberException : Exception
{
    public NegativeNumberException(string message) : base(message)
    {

    }
}
