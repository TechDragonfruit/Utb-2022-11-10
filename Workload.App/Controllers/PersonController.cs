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

    [HttpPut]
    public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonRequest request)
    {
        return await CallMediator(request);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonById(Guid id)
    {
        return await CallMediator(GetPersonRequest.Instance(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPeople()
    {
        return await CallMediator(GetPeopleRequest.Instance);
    }
}
