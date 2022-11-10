namespace EnergiBolaget.Auth.Mediator;

using MediatR;

public record RegisterUserRequest(string Email, string FirstName, string LastName, string PhoneNumber, string Password) : IRequest<MediatorResponse>;
public record LoginUserRequest(string UserName, string Password) : IRequest<MediatorResponse>;
public record ResetPasswordRequest(string UserName, string NewPassword) : IRequest<MediatorResponse>;
public record SetMyInfoRequest(string UserName, string Email, string FirstName, string LastName, string PhoneNumber, string Password) : IRequest<MediatorResponse>;
//public record VerifyEmailRequest(string UserName, string Email, string Token) : IRequest<MediatorResponse>;
//public record VerifyPhoneNumberRequest(string UserName, string PhoneNumber, string Token) : IRequest<MediatorResponse>;

public record GetUsersRequest() : IRequest<MediatorResponse>;
public record GetUserRequest(string UserName) : IRequest<MediatorResponse>;
public record CreateRoleRequest(string Name) : IRequest<MediatorResponse>;
public record GetRolesRequest() : IRequest<MediatorResponse>;
public record AddUserToRoleRequest(string UserName, string Role) : IRequest<MediatorResponse>;
