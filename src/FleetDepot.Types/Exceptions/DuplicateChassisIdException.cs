namespace FleetDepot.Types.Exceptions;

public class DuplicateChassisIdException : Exception
{
    public DuplicateChassisIdException(string message, Exception innerException = null)
        : base(message, innerException) { }
}
