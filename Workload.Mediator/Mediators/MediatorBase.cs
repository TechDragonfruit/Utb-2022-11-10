namespace Workload.Mediator.Mediators;

using Workload.Contract;

public class MediatorBase
{
    protected virtual MediatorResponse CreateResponse(int status, string message, object? data)
    {
        return new MediatorResponse { Status = status, Message = message, Data = data };
    }
}



