using Microsoft.AspNetCore.Http;
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

        /// <summary> Checks if signal light is on. (measuring data and sending it to mqtt broker). </summary>
        /// <response code="200">Operation completed successfully and boolean result is returned. </response>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult IsOn()
        {
            return Ok(_signalLightService.IsOn);
        }

        /// <summary>Returns the color of current active light. </summary>
        /// <response code="200">Signal light color represented as a string. </response>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult ActiveColor()
        {
            return Ok(_signalLightService.ActiveColor.ToString());
        }
        
        /// <summary>Updates active color of signal light. </summary>
        /// <response code="200">Signal light color is changed to new value.</response>
        /// <param name="color">New color value. Color value is LightColor enum.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/api/[controller]/ActiveColor/{color}")]
        public IActionResult ChangeActiveColor(LightColor color)
        {
            _signalLightService.ChangeColor(color);
            return Ok();
        }

        /// <summary>Turns on the signal light. </summary>
        /// <remarks>Signal light is started with default color that is configured in appsettings.json</remarks>
        /// <response code="200">Signal light started</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public IActionResult TurnOn()
        {
            _signalLightService.TurnOn();
            return Ok();
        }
        
        /// <summary>Turns off the signal light. </summary>
        /// <remarks>Current color or signal light is not saved. Color will be default one at next start</remarks>
        /// <response code="200">Signal light turned off.</response>
        [HttpPut]
        public IActionResult TurnOff()
        {
            _signalLightService.TurnOff();
            return Ok();
        }
        
        /// <summary> Returns active configuration on witch application is running. </summary>
        /// <response code="200">Configuration fetched successfully and returned.</response>
        [ProducesResponseType(typeof(SignalLightOptions), StatusCodes.Status200OK)]
        [HttpGet("/api/[controller]/parameters")]
        public IActionResult GetActiveParameters()
        {
            return Ok(_configurationService.ActiveConfiguration);
        }
        
        /// <summary>Updates the name of signal light. </summary>
        /// <param name="name">New name.</param>
        /// <response code="200">Signal light is renamed.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/api/[controller]/parameters/{name}")]
        public IActionResult UpdateName(string name)
        {
            _configurationService.UpdateName(name);
            return Ok();
        }
        
        /// <summary>Updates default color of signal light. </summary>
        /// <response code="200">Signal light always started with default color.</response>
        /// <param name="defaultLight">New default color value.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/api/[controller]/parameters/{defaultLight}")]
        public IActionResult UpdateDefaultLight(LightColor defaultLight)
        {
            _configurationService.UpdateDefaultLight(defaultLight);
            return Ok();
        }
    }
}