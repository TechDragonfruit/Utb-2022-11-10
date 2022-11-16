namespace Workload.App.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Workload.Contract;

public class MediatorControllerBase : ControllerBase
{
    private readonly IMediator mediator;

    public MediatorControllerBase(IMediator mediator)
    {
        this.mediator = mediator;
    }
    protected virtual async Task<IActionResult> CallMediator(object request)
    {
        //TODO Add logging of response.message
        return await mediator.Send(request) is not MediatorResponse response
            ? StatusCode(500, null)
            : (IActionResult)StatusCode(response.Status, response.Data);
    }
}