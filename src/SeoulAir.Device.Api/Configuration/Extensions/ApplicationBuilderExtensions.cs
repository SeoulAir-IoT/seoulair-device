using Microsoft.AspNetCore.Builder;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Api.Configuration.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwaggerUI(config =>
        {
            config.SwaggerEndpoint(SwaggerEndpoint, OpenApiInfoProjectName);
            config.RoutePrefix = string.Empty;
            config.DocumentTitle = OpenApiInfoTitle;
        });
        app.UseSwagger();

        return app;
    }
}
