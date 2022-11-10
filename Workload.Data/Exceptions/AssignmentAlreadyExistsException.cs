namespace Workload.Data.Exceptions;

using System.Runtime.Serialization;

[Serializable]
public class AssignmentAlreadyExistsException : Exception
{
    public AssignmentAlreadyExistsException()
    {
    }

    public AssignmentAlreadyExistsException(string? message) : base(message)
    {
    }

    public AssignmentAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AssignmentAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}