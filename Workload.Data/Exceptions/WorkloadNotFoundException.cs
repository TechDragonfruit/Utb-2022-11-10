namespace Workload.Data.Exceptions;

using System;
using System.Runtime.Serialization;

[Serializable]
public class WorkloadNotFoundException : Exception
{
    public WorkloadNotFoundException()
    {
    }

    public WorkloadNotFoundException(string? message) : base(message)
    {
    }

    public WorkloadNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected WorkloadNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}