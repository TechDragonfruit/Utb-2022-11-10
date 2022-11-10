namespace EnergiBolaget.Auth.Mediator;
using System;
using System.Collections.Generic;

public record RegisterUserResponse(Guid Id);
public record LoginUserResponse(string Token, Guid IdentityId);
public record ResetPasswordResponse();
public record GetMyInfoResponse();
public record SetMyInfoResponse();

public record CreateRoleResponse();
public record GetRolesResponse(IEnumerable<string> Roles);
public record AddUserToRoleResponse();




public record UserInfoResponse(Guid Id, string FirstName, string LastName, string UserName, string NormalizedUserName,
    string Email, string NormalizedEmail, bool EmailConfirmed, string PhoneNumber, bool PhoneNumberConfirmed, bool TwoFactorEnabled,
    DateTimeOffset? LockoutEnd, bool LockoutEnabled, int AccessFailedCount);

public record UpdateMyInfoResponse();