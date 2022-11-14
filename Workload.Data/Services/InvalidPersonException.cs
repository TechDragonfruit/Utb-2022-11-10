namespace Workload.Data.Services;

using System;
using System.Runtime.Serialization;

[Serializable]
internal class InvalidPersonException : Exception
{
    public InvalidPersonException()
    {
    }

    public InvalidPersonException(string? message) : base(message)
    {
    }

    public InvalidPersonException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidPersonException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}