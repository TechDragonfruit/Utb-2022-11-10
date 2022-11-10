namespace Workload.Data.Exceptions;

using System;
using System.Runtime.Serialization;

[Serializable]
public class AssignmentNotFoundException : Exception
{
    public AssignmentNotFoundException()
    {
    }

    public AssignmentNotFoundException(string? message) : base(message)
    {
    }

    public AssignmentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AssignmentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}