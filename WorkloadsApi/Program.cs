using EnergiBolaget.Auth.Extensions;

using Microsoft.OpenApi.Models;

using Workload.App;
using Workload.Auth.Configuration;
using Workload.Data;

using WorkloadsApi.HealthChecks;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks()
    .AddCheck("ICMP_01", new ICMPHealthCheck("www.microsoft.com", 100))
    .AddCheck("ICMP_02", new ICMPHealthCheck("www.google.com", 100));
//    .AddCheck("ICMP_03", new ICMPHealthCheck($"www.{Guid.NewGuid():N}.com", 100));

builder.Services.AddAppPart(() => builder.Configuration.GetConnectionString("WorkloadDb"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuth(builder.Configuration.GetConnectionString("IdentityDb"), builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});

WebApplication app = builder.Build();

app.UpdateIdentityDb();

using (IServiceScope scope = app.Services.CreateScope())
{
    _ = scope.ServiceProvider.GetRequiredService<IWorkloadDbContext>().EnsureDbExists();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks(new PathString("/health"), new CustomHealthCheckOptions());

app.MapControllers();

app.Run();
