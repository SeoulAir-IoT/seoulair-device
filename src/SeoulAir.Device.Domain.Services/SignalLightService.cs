using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Enums;
using SeoulAir.Device.Domain.Extensions;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Options;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Domain.Services
{
    public class SignalLightService : ISignalLightService
    {
        private readonly SignalLightOptions _options;
        private readonly Dictionary<string, StationSignalLight> _stationSignalLights;

        public SignalLightService(IOptions<SignalLightOptions> options)
        {
            _options = options.Value;
            _stationSignalLights = InitializeLights(_options.StationCodes);
        }

        private Dictionary<string, StationSignalLight> InitializeLights(List<string> stationCodes)
        {
            var stationSignalLights = new Dictionary<string, StationSignalLight>(stationCodes.Count);
            foreach (string stationCode in stationCodes)
                stationSignalLights.Add(stationCode, new StationSignalLight
                {
                    ActiveColor = LightColor.Black,
                    IsOn = false,
                    StationCode = stationCode
                });

            return stationSignalLights;
        }

        public bool IsOn(string stationCode)
        {
            if (!_stationSignalLights.ContainsKey(stationCode))
                throw new ArgumentNullException(string.Format(StationCodeDoesNotExistMessage, stationCode)
                    .FormatAsExceptionMessage());
            
            return _stationSignalLights[stationCode].IsOn;
        }

        public LightColor GetActiveColor(string stationCode)
        {
            if (!_stationSignalLights.ContainsKey(stationCode))
                throw new ArgumentNullException(string.Format(StationCodeDoesNotExistMessage, stationCode)
                    .FormatAsExceptionMessage());
            
            return _stationSignalLights[stationCode].ActiveColor;
        }

        public void TurnOn(string stationCode)
        {
            if (_stationSignalLights.ContainsKey(stationCode))
            {
                _stationSignalLights[stationCode].IsOn = true;
                _stationSignalLights[stationCode].ActiveColor = _options.DefaultColor;
            }
        }

        public void TurnOff(string stationCode)
        {
            if (_stationSignalLights.ContainsKey(stationCode))
            {
                _stationSignalLights[stationCode].IsOn = false;
                _stationSignalLights[stationCode].ActiveColor = LightColor.Black;
            }
        }

        public void ChangeColor(string stationCode, LightColor color)
        {
            if (!_stationSignalLights.ContainsKey(stationCode))
                throw new InvalidOperationException(ChangeColorExceptionMessage);
            
            if (!_stationSignalLights[stationCode].IsOn)
                throw new InvalidOperationException(ChangeColorExceptionMessage);

            _stationSignalLights[stationCode].ActiveColor = color;
        }
    }
}