namespace EnergiBolaget.Auth.Services;
public interface IAuthService
{
}

public class AuthService : IAuthService
{
    private readonly IAuthDbContext context;

    public AuthService(IAuthDbContext context)
    {
        this.context = context;
    }
}

public interface IUser
{
    Task Test();
}

public interface IAdmin
{
    Task Test();
}

public class Service : IAdmin, IUser
{
    public Task Test()
    {
        throw new NotImplementedException();
    }
}

