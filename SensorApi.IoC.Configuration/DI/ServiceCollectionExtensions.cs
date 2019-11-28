using AutoMapper;
using SensorApi.API.Common.Settings;
using SensorApi.IoC.Configuration.AutoMapper;
using SensorApi.Services;
using SensorApi.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SensorApi.IoC.Configuration.DI
{
    public static class ServiceCollectionExtensions
    {
        public static AppSettings ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection(nameof(AppSettings));
            if (appSettingsSection == null)
                throw new System.Exception("No appsettings section has been found");

            var appSettings = appSettingsSection.Get<AppSettings>();

            if (!appSettings.IsValid())
                throw new Exception("No valid settings.");

            services.Configure<AppSettings>(appSettingsSection);

            //Automap settings
            services.AddAutoMapper();
            MappingConfigurationsHelper.ConfigureMapper();

            services.AddTransient<IUserService, UserService>();

            return appSettings;
        }
    }
}
