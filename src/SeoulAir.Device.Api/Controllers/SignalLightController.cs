using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Client;
using SeoulAir.Device.Client.Enums;
using SeoulAir.Device.Domain.Interfaces.Services;
using System;

namespace SeoulAir.Device.Api.Controllers
{
    [ApiController]
    [Route(ISignalLightApi.ApiBase)]
    public class SignalLightController : ControllerBase, ISignalLightApi
    {
        private readonly ISignalLightService _signalLightService;
        private readonly ISignalLightConfigurationService _configurationService;

        public SignalLightController(
            ISignalLightService signalLightService,
            ISignalLightConfigurationService configurationService)
        {
            _signalLightService = signalLightService;
            _configurationService = configurationService;
        }

        [HttpGet(ISignalLightApi.GetActiveColorPath)]
        public SignalLightColors GetActiveColor(string stationCode)
        {
            return _signalLightService.GetActiveColor(stationCode);
        }

        public void ChangeActiveColor(string stationCode, SignalLightColors color)
        {
            _signalLightService.ChangeColor(stationCode, color);
        }

        public bool IsOn(string stationCode)
        {
            return _signalLightService.IsOn(stationCode);
        }

        public void TurnOn(string stationCode)
        {
            _signalLightService.TurnOn(stationCode);
        }

        public void TurnOff(string stationCode)
        {
            _signalLightService.TurnOff(stationCode);
        }

        public void GetConfiguration(string stationCode)
        {
            _configurationService.ActiveConfiguration;
        }

        public void UpdateName(string stationCode, string newName)
        {
            throw new NotImplementedException();
        }

        public void UpdateDefaultColor(string stationCode, SignalLightColors newDefaultColor)
        {
            throw new NotImplementedException();
        }
    }
}