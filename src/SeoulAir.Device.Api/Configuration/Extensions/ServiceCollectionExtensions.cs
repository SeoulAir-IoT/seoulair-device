using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SeoulAir.Device.Api.HelperClasses;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
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

        public static IServiceCollection AddApplicationSettings(this IServiceCollection services,IConfiguration configuration)
        {
            ISettingsReader reader = new SettingsReader(configuration);

            AppSettings settings = reader.ReadAllSettings();
            services.AddSingleton(settings);

            return services;
        }

    }
}
