using System;
using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Enums;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Options;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Domain.Services
{
    public class SignalLightService : ISignalLightService
    {
        private readonly SignalLightOptions _options;
        
        public bool IsOn { get; private set; }
        public LightColor ActiveColor { get; private set; }

        public SignalLightService(IOptions<SignalLightOptions> options)
        {
            _options = options.Value;
            IsOn = false;
            ActiveColor = LightColor.Black;
        }
        
        public void TurnOn()
        {
            IsOn = true;
            ActiveColor = _options.DefaultColor;
        }

        public void TurnOff()
        {
            IsOn = false;
            ActiveColor = LightColor.Black;
        }

        public void ChangeColor(LightColor color)
        {
            if (!IsOn)
                throw new InvalidOperationException(ChangeColorExceptionMessage);
            ActiveColor = color;
        }
    }
}