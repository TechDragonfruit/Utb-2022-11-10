namespace Workload.App.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Workload.Contract;

[ApiController]
[Route("[controller]/[action]")]
public class AssignmentController : MediatorControllerBase
{
    public AssignmentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentRequest request)
    {
        return await CallMediator(request);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssignmentById(Guid id)
    {
        return await CallMediator(new GetAssignmentRequest(id));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAssignments()
    {
        return await CallMediator(new GetAssignmentsRequest());
    }
}
