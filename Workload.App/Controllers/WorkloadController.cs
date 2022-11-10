namespace Workload.App.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Workload.Contract;

[ApiController]
[Route("[controller]/[action]")]
public class WorkloadController : MediatorControllerBase
{
    public WorkloadController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkload([FromBody] CreateWorkloadRequest request)
    {
        return await CallMediator(request);
    }
}
