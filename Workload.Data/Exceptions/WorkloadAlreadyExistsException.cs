namespace Workload.Data.Exceptions;

using System;
using System.Runtime.Serialization;

[Serializable]
public class WorkloadAlreadyExistsException : Exception
{
    public WorkloadAlreadyExistsException()
    {
    }

    public WorkloadAlreadyExistsException(string? message) : base(message)
    {
    }

    public WorkloadAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected WorkloadAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}