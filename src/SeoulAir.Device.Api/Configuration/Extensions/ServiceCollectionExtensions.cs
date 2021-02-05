using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SeoulAir.Device.Domain.Options;
using SeoulAir.Device.Domain.Services.OptionsValidators;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Api.Configuration.Extensions
{
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
                        Name = GitlabContactName,
                        Url = new Uri(GitlabRepoUri)
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddApplicationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AirQualitySensorOptions>(
                configuration.GetSection(AirQualitySensorOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<AirQualitySensorOptions>, AirQualitySensorOptionsValidator>();

            services.Configure<SignalLightOptions>(
                configuration.GetSection(SignalLightOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<SignalLightOptions>, SignalLightOptionsValidator>();

            services.Configure<MqttConnectionOptions>(
                configuration.GetSection(MqttConnectionOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<MqttConnectionOptions>, MqttOptionsValidator>();

            return services;
        }
    }
}
