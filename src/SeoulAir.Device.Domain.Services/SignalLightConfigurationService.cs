using System;
using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Enums;
using SeoulAir.Device.Domain.Extensions;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Options;
using SeoulAir.Device.Domain.Resources;

namespace SeoulAir.Device.Domain.Services
{
    public class SignalLightConfigurationService : ISignalLightConfigurationService
    {
        private readonly IOptions<SignalLightOptions> _options;

        public SignalLightConfigurationService(IOptions<SignalLightOptions> options)
        {
            _options = options;
        }

        public SignalLightOptions ActiveConfiguration => _options.Value;

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(string.Format(Strings.ParameterNullOrEmptyMessage, nameof(name))
                    .FormatAsExceptionMessage());

            _options.Value.Name = name;
        }

        public void UpdateDefaultLight(LightColor color)
        {
            _options.Value.DefaultColor = color;
        }
    }
}