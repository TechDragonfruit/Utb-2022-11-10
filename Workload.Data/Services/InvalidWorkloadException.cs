namespace Workload.Data.Services;

using System;
using System.Runtime.Serialization;

[Serializable]
internal class InvalidWorkloadException : Exception
{
    public InvalidWorkloadException()
    {
    }

    public InvalidWorkloadException(string? message) : base(message)
    {
    }

    public InvalidWorkloadException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidWorkloadException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}