using EnergiBolaget.Auth.Mediator;

namespace Workload.Auth.Mediator;

using AutoMapper;

using EnergiBolaget.Auth.Model;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Workload.Auth.Configuration;

public class AuthMediator :
    IRequestHandler<RegisterUserRequest, MediatorResponse>,
    IRequestHandler<LoginUserRequest, MediatorResponse>,
    IRequestHandler<ResetPasswordRequest, MediatorResponse>,
    IRequestHandler<SetMyInfoRequest, MediatorResponse>,
    //IRequestHandler<VerifyEmailRequest, MediatorResponse>,
    //IRequestHandler<VerifyPhoneNumberRequest, MediatorResponse>,

    IRequestHandler<GetUsersRequest, MediatorResponse>,
    IRequestHandler<GetUserRequest, MediatorResponse>,
    IRequestHandler<CreateRoleRequest, MediatorResponse>,
    IRequestHandler<GetRolesRequest, MediatorResponse>,
    IRequestHandler<AddUserToRoleRequest, MediatorResponse>
{

    private readonly IMapper mapper;
    private readonly SignInManager<User> signinManager;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;
    private readonly JwtSettings jwtSettings;

    public AuthMediator(IMapper mapper, SignInManager<User> signinManager, UserManager<User> userManager, RoleManager<Role> roleManager, IOptionsSnapshot<JwtSettings> jwtOptions)
    {
        this.mapper = mapper;
        this.signinManager = signinManager;
        this.userManager = userManager;
        this.roleManager = roleManager;
        jwtSettings = jwtOptions.Value;
    }

    public async Task<MediatorResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        if (userManager.Users.Any(u => u.Email == request.Email))
        {
            return CreateMediatorResponse(false, "User already registered", null);
        }

        User user = mapper.Map<RegisterUserRequest, User>(request);
        //TODO Implement this functionality?
        user.EmailConfirmed = true;
        user.PhoneNumberConfirmed = true;

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return CreateMediatorResponse(false, CombineErrors(result.Errors), null);
        }

        result = await userManager.AddToRoleAsync(user, "User");
        if (!result.Succeeded)
        {
            return CreateMediatorResponse(false, CombineErrors(result.Errors), null);
        }

        //No other admins exists, make this the first one
        if (!(await userManager.GetUsersInRoleAsync("Admin")).Any())
        {
            result = await userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                return CreateMediatorResponse(false, CombineErrors(result.Errors), null);
            }
        }

        return CreateMediatorResponse(true, "User registered", new RegisterUserResponse(user.Id));
    }

    public async Task<MediatorResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        User? user = GetUser(request.UserName);
        if (user is null || !user.EmailConfirmed || !user.PhoneNumberConfirmed)
        {
            return CreateMediatorResponse(false, "User not found or confirmed!", null);
        }

        bool userSigninResult = await userManager.CheckPasswordAsync(user, request.Password);
        if (!userSigninResult)
        {
            return CreateMediatorResponse(false, "User or password incorrect!", null);
        }

        IList<string> roles = await userManager.GetRolesAsync(user);
        string jwt = GenerateJwt(user, roles);

        return string.IsNullOrWhiteSpace(jwt)
            ? CreateMediatorResponse(false, "Token not generated", null)
            : CreateMediatorResponse(true, "User logged in", new LoginUserResponse(jwt, user.Id));
    }

    public async Task<MediatorResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        User? user = GetUser(request.UserName);
        if (user is null)
        {
            return CreateMediatorResponse(false, "User not found", null);
        }

        //TODO Make this a two step action
        string passwordToken = await userManager.GeneratePasswordResetTokenAsync(user);
        IdentityResult result = await userManager.ResetPasswordAsync(user, passwordToken, request.NewPassword);

        return !result.Succeeded
            ? CreateMediatorResponse(false, CombineErrors(result.Errors), null)
            : CreateMediatorResponse(true, "User added to role", new AddUserToRoleResponse());
    }

    public async Task<MediatorResponse> Handle(SetMyInfoRequest request, CancellationToken cancellationToken)
    {
        User? user = GetUser(request.UserName);
        if (user is null)
        {
            return CreateMediatorResponse(false, "User not found", null);
        }

        IdentityResult result = await userManager.SetEmailAsync(user, request.Email);
        if (!result.Succeeded)
        {
            return CreateMediatorResponse(false, CombineErrors(result.Errors), null);
        }

        string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return CreateMediatorResponse(false, CombineErrors(result.Errors), null);
        }

        result = await userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        if (!result.Succeeded)
        {
            return CreateMediatorResponse(false, CombineErrors(result.Errors), null);
        }

        token = await userManager.GenerateChangePhoneNumberTokenAsync(user, request.PhoneNumber);

        result = await userManager.ChangePhoneNumberAsync(user, request.PhoneNumber, token);


        return !result.Succeeded
            ? CreateMediatorResponse(false, CombineErrors(result.Errors), null)
            : CreateMediatorResponse(true, "User updated", mapper.Map<UserInfoResponse>(user));
    }

    //public async Task<MediatorResponse> Handle(VerifyEmailRequest request, CancellationToken cancellationToken)
    //{
    //    User? user = GetUser(request.UserName);

    //    if (user is null)
    //    {
    //        return CreateMediatorResponse(false, ErrorDetail.Create("User not found"));
    //    }

    //    IdentityResult result = await userManager.ChangeEmailAsync(user, request.Email, request.Token);

    //    return !result.Succeeded
    //        ? CreateMediatorResponse(false, ErrorDetail.Create(CombineErrors(result.Errors)))
    //        : CreateMediatorResponse(true, mapper.Map<User, UserInfoResponse>(user));
    //}

    //public async Task<MediatorResponse> Handle(VerifyPhoneNumberRequest request, CancellationToken cancellationToken)
    //{
    //    User? user = GetUser(request.UserName);
    //    if (user is null)
    //    {
    //        return CreateMediatorResponse(false, ErrorDetail.Create("User not found"));
    //    }

    //    IdentityResult result = await userManager.ChangePhoneNumberAsync(user, request.PhoneNumber, request.Token);

    //    return !result.Succeeded
    //        ? CreateMediatorResponse(false, ErrorDetail.Create(CombineErrors(result.Errors)))
    //        : CreateMediatorResponse(true, mapper.Map<User, UserInfoResponse>(user));
    //}

    public Task<MediatorResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<UserInfoResponse> users = userManager.Users.Select(u => mapper.Map<UserInfoResponse>(u));

        return Task.FromResult(CreateMediatorResponse(true, "Found users", users));
    }

    public Task<MediatorResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        User? user = GetUser(request.UserName);
        if (user is null)
        {
            return Task.FromResult(CreateMediatorResponse(false, "User not found", null));
        }

        UserInfoResponse response = mapper.Map<User, UserInfoResponse>(user);

        return Task.FromResult(CreateMediatorResponse(true, "Found user", response));
    }

    public async Task<MediatorResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        Role role = mapper.Map<CreateRoleRequest, Role>(request);

        IdentityResult roleResult = await roleManager.CreateAsync(role);

        return !roleResult.Succeeded
            ? CreateMediatorResponse(false, CombineErrors(roleResult.Errors), null)
            : CreateMediatorResponse(true, "Role created", new CreateRoleResponse());
    }

    public Task<MediatorResponse> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<string> roles = roleManager.Roles.Select(r => r.Name);

        return !roles.Any()
            ? Task.FromResult(CreateMediatorResponse(false, "Roles not found", null))
            : Task.FromResult(CreateMediatorResponse(true, "Found roles", new GetRolesResponse(roles)));
    }

    public async Task<MediatorResponse> Handle(AddUserToRoleRequest request, CancellationToken cancellationToken)
    {
        User? user = GetUser(request.UserName);
        if (user is null)
        {
            return CreateMediatorResponse(false, "User not found", null);
        }

        IdentityResult result = await userManager.AddToRoleAsync(user, request.Role);

        return !result.Succeeded
            ? CreateMediatorResponse(false, result.Errors.First().Description, null)
            : CreateMediatorResponse(true, "User added", new AddUserToRoleResponse());
    }



    private string GenerateJwt(User user, IList<string> roles)
    {
        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        IEnumerable<Claim> roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
        claims.AddRange(roleClaims);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        DateTime expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings.ExpirationInDays));

        JwtSecurityToken token = new(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Issuer,
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User? GetUser(string userName)
    {
        User? user = userManager.Users.SingleOrDefault(u => u.UserName == userName);
        return user;
    }

    private string CombineErrors(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(e => e.Description));
    }

    private MediatorResponse CreateMediatorResponse(bool succeeded, string message, object? response)
    {
        return new MediatorResponse { Success = succeeded, Message = message, Data = response };
    }
}
