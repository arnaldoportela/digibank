using Microsoft.OpenApi.Models;

namespace Account_MS.Configurations;

public static class SwaggerConfig
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Accounts", Version = "V1" });
            options.EnableAnnotations();
            options.DescribeAllParametersInCamelCase();
        });
    }
}
