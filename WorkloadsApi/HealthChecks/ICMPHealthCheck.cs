namespace WorkloadsApi.HealthChecks;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Text.Json;

public class ICMPHealthCheck : IHealthCheck
{
    private readonly string Host;
    private readonly int HealthyRoundtripTime;

    public ICMPHealthCheck(string host, int healthyRoundtripTime)
    {
        Host = host;
        HealthyRoundtripTime = healthyRoundtripTime;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using Ping ping = new();
            PingReply reply = await ping.SendPingAsync(Host);
            switch (reply.Status)
            {
                case IPStatus.Success:
                    string msg = $"ICMP to {Host} took {reply.RoundtripTime} ms.";
                    return reply.RoundtripTime > HealthyRoundtripTime
                        ? HealthCheckResult.Degraded(msg)
                        : HealthCheckResult.Healthy(msg);
                default:
                    string err = $"ICMP to {Host} failed: {reply.Status}";
                    return HealthCheckResult.Unhealthy(err);
            }
        }
        catch (Exception e)
        {
            string err = $"ICMP to {Host} failed: {e.Message}";
            return HealthCheckResult.Unhealthy(err);
        }
    }
}

public class CustomHealthCheckOptions : HealthCheckOptions
{
    public CustomHealthCheckOptions()
        : base()
    {
        JsonSerializerOptions jsonSerializerOptions = new()
        {
            WriteIndented = true
        };

        ResponseWriter = async (c, r) =>
        {
            c.Response.ContentType =
            MediaTypeNames.Application.Json;
            c.Response.StatusCode = StatusCodes.Status200OK;
            string result = JsonSerializer.Serialize(new
            {
                checks = r.Entries.Select(e => new
                {
                    name = e.Key,
                    responseTime =
                e.Value.Duration.TotalMilliseconds,
                    status = e.Value.Status.ToString(),
                    description = e.Value.Description
                }),
                totalStatus = r.Status,
                totalResponseTime = r.TotalDuration.TotalMilliseconds,
            }, jsonSerializerOptions);
            await c.Response.WriteAsync(result);
        };
    }
}