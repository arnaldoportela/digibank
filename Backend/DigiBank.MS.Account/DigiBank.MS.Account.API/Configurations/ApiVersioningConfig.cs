using Asp.Versioning;

namespace DigiBank.MS.Account.API.Configurations;

public static class ApiVersioningConfig
{
    public static void AddApiVersioningConfig(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = ApiVersion.Default;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}
