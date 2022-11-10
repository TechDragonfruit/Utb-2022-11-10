namespace EnergiBolaget.Auth.Controllers;

using EnergiBolaget.Auth.Mediator;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Threading.Tasks;

[Route("[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        return await CallMediator(request);
    }

    [HttpPost]
    public async Task<IActionResult> Signin(LoginUserRequest request)
    {
        return await CallMediator(request);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        return await CallMediator(request);
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> GetMyInfo()
    {
        string userName = User.FindFirstValue(ClaimTypes.Name);

        return string.IsNullOrWhiteSpace(userName)
            ? BadRequest(ErrorDetail.Create("Not signed in"))
            : await CallMediator(new GetUserRequest(userName));
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> SetMyInfo([FromBody] SetMyInfoRequest request)
    {
        //SetMyInfoRequest2 request2 = new(request.UserName, request.Email, request.FirstName, request.LastName, request.PhoneNumber, request.Password, Request.GetDisplayUrl());
        return await CallMediator(request);
    }

    //[HttpPost("{userName}&{email}&{token}")]
    //public async Task<IActionResult> VerifyEmail(string userName, string email, string token)
    //{
    //    VerifyEmailRequest request = new(userName, email, System.Net.WebUtility.UrlDecode(token));
    //    return await CallMediator(request);
    //}

    //[HttpPost("{userName}&{phonenumber}&{token}")]
    //public async Task<IActionResult> VerifyPhoneNumber(string userName, string phoneNumber, string token)
    //{
    //    VerifyPhoneNumberRequest request = new(userName, phoneNumber, System.Net.WebUtility.UrlDecode(token));
    //    return await CallMediator(request);
    //}

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return await CallMediator(new GetUsersRequest());
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpGet("{userName}")]
    public async Task<IActionResult> GetUser(string userName)
    {
        return await CallMediator(new GetUserRequest(userName));
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        return await CallMediator(request);
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpGet("getroles")]
    public async Task<IActionResult> GetRoles()
    {
        return await CallMediator(new GetRolesRequest());
    }

    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddUserToRole(AddUserToRoleRequest request)
    {
        return await CallMediator(request);
    }

    private async Task<IActionResult> CallMediator(object request)
    {
        return await mediator.Send(request) is not MediatorResponse response
            ? BadRequest(new MediatorResponse { Success = false, Message = "No response", Data = null })
            : response.Success
            ? Ok(response)
            : BadRequest(response);
    }
}
