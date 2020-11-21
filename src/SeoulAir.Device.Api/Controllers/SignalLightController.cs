using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Domain.Enums;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Options;

namespace SeoulAir.Device.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SignalLightController : ControllerBase
    {
        private readonly ISignalLightService _signalLightService;
        private readonly ISignalLightConfigurationService _configurationService;

        public SignalLightController(ISignalLightService signalLightService,
            ISignalLightConfigurationService configurationService)
        {
            _signalLightService = signalLightService;
            _configurationService = configurationService;
        }

        [HttpGet]
        public IActionResult IsOn()
        {
            return Ok(_signalLightService.IsOn);
        }

        [HttpGet]
        public IActionResult ActiveColor()
        {
            return Ok(_signalLightService.ActiveColor.ToString());
        }
        
        [HttpPut]
        public IActionResult ChangeActiveColor(LightColor color)
        {
            _signalLightService.ChangeColor(color);
            return Ok();
        }

        [HttpPut]
        public IActionResult TurnOn()
        {
            _signalLightService.TurnOn();
            return Ok();
        }
        
        [HttpPut]
        public IActionResult TurnOff()
        {
            _signalLightService.TurnOff();
            return Ok();
        }

        [HttpGet("/api/[controller]/parameters")]
        public IActionResult GetActiveParameters()
        {
            return Ok(_configurationService.ActiveConfiguration);
        }
        
        [HttpPut("/api/[controller]/parameters/name")]
        public IActionResult UpdateName(string name)
        {
            _configurationService.UpdateName(name);
            return Ok();
        }
        
        [HttpPut("/api/[controller]/parameters/defaultLight")]
        public IActionResult UpdateDefaultLight(LightColor color)
        {
            _configurationService.UpdateDefaultLight(color);
            return Ok();
        }
    }
}