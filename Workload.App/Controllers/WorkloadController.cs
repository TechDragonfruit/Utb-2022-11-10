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

    [HttpPut]
    public async Task<IActionResult> UpdateWorkload([FromBody] UpdateWorkloadRequest request)
    {
        return await CallMediator(request);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkloadById(Guid id)
    {
        return await CallMediator(GetWorkloadRequest.Instance(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWorkloads()
    {
        return await CallMediator(GetWorkloadsRequest.Instance);
    }
}
