namespace EnergiBolaget.Auth.Extensions;

using EnergiBolaget.Auth;
using EnergiBolaget.Auth.Mappings;
using EnergiBolaget.Auth.Model;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Text;

using Workload.Auth.Configuration;

public static class AuthExtensions
{
    public static IHost UpdateIdentityDb(this IHost host)
    {
        using (IServiceScope scope = host.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<AuthDbContext>().EnsureDbExists();
        }

        return host;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, string connectionString, JwtSettings jwtSettings)
    {
        _ = services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

        _ = services.AddIdentity<User, Role>(options =>
        {
            //TODO Lowered password complexity...
            //https://stackoverflow.com/questions/39825634/how-override-asp-net-core-identitys-password-policy
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        _ = services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(AuthExtensions).Assembly);

        _ = services.AddMediatR(typeof(AuthExtensions).Assembly);

        _ = services.AddControllers().AddApplicationPart(typeof(AuthExtensions).Assembly);

        _ = services.AddAuthorization()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers/tree/dev/src
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
    {
        _ = app.UseAuthentication();

        _ = app.UseAuthorization();

        return app;
    }
}
