namespace Workload.Data.Exceptions;

using System;
using System.Runtime.Serialization;

[Serializable]
internal class InvalidAssignmentException : Exception
{
    public InvalidAssignmentException()
    {
    }

    public InvalidAssignmentException(string? message) : base(message)
    {
    }

    public InvalidAssignmentException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidAssignmentException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}