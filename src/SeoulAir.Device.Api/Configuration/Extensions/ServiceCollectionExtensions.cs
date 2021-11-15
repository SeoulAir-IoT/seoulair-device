using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SeoulAir.Device.Api.Configuration.Validators;
using SeoulAir.Device.Domain.Options;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Api.Configuration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.ConfigureSwaggerGen(options =>
        {
            var xmlDocumentationFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocumentationFileName));
            options.DescribeAllParametersInCamelCase();
            options.SwaggerDoc(OpenApiInfoProjectVersion, new OpenApiInfo
            {
                Title = OpenApiInfoTitle,
                Description = OpenApiInfoDescription,
                Version = OpenApiInfoProjectVersion,
                Contact = new OpenApiContact
                {
                    Email = string.Empty,
                    Name = GithubContactName,
                    Url = new Uri(GithubRepositoryUrl)
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddApplicationSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SeoulAirDeviceOptions>(configuration.GetSection(SeoulAirDeviceOptions.OptionsPath));
        services.AddSingleton<IValidateOptions<SeoulAirDeviceOptions>, SeoulAirDeviceOptionsValidator>();

        services.Configure<MqttConnectionOptions>(configuration.GetSection(MqttConnectionOptions.OptionsPath));
        services.AddSingleton<IValidateOptions<MqttConnectionOptions>, MqttOptionsValidator>();

        return services;
    }
}
