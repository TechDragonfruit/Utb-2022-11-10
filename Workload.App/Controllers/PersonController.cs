namespace Workload.App.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Workload.Contract;

[ApiController]
[Route("[controller]/[action]")]
public class PersonController : MediatorControllerBase
{
    public PersonController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request)
    {
        return await CallMediator(request);
    }
}
