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

        /// <summary> Checks if signal light is on for the specific station.</summary>
        /// <response code="200">Operation completed successfully and boolean result is returned.</response>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpGet("{stationCode}")]
        public ActionResult<bool> IsOn(string stationCode)
        {
            return Ok(_signalLightService.IsOn(stationCode));
        }

        /// <summary>Returns the color of active light for specified station code.</summary>
        /// <response code="200">Signal light color represented as a string.</response>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("{stationCode}")]
        public ActionResult<string> ActiveColor(string stationCode)
        {
            return Ok(_signalLightService.GetActiveColor(stationCode).ToString());
        }

        /// <summary>Updates active color of signal light for specified station code.</summary>
        /// <response code="204">Signal light color is changed to new value.</response>
        /// <param name="stationCode">Station code of the light that needs to be changed</param>
        /// <param name="color">New color value. Color value is LightColor enum.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/api/[controller]/ActiveColor/{stationCode}/{color}")]
        public IActionResult ChangeActiveColor(string stationCode, LightColor color)
        {
            _signalLightService.ChangeColor(stationCode, color);
            return NoContent();
        }

        /// <summary>Turns on the signal light for specified station code.</summary>
        /// <remarks>Signal light is started with default color that is configured in appsettings.json</remarks>
        /// <response code="204">Signal light started</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("{stationCode}")]
        public IActionResult TurnOn(string stationCode)
        {
            _signalLightService.TurnOn(stationCode);
            return NoContent();
        }
        
        /// <summary>Turns off the signal light for specified station code.</summary>
        /// <remarks>Current color or signal light is not saved. Color will be default one at next start</remarks>
        /// <response code="204">Signal light turned off.</response>
        [HttpPut("{stationCode}")]
        public IActionResult TurnOff(string stationCode)
        {
            _signalLightService.TurnOff(stationCode);
            return NoContent();
        }
        
        /// <summary> Returns active configuration on witch application is running. </summary>
        /// <response code="200">Configuration fetched successfully and returned.</response>
        [ProducesResponseType(typeof(SignalLightOptions), StatusCodes.Status200OK)]
        [HttpGet("/api/[controller]/parameters")]
        public ActionResult<SignalLightOptions> GetActiveParameters()
        {
            return Ok(_configurationService.ActiveConfiguration);
        }
        
        /// <summary>Updates the name of signal light. </summary>
        /// <param name="name">New name.</param>
        /// <response code="204">Signal light is renamed.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/api/[controller]/parameters/{name}")]
        public IActionResult UpdateName(string name)
        {
            _configurationService.UpdateName(name);
            return NoContent();
        }
        
        /// <summary>Updates default color of signal light. </summary>
        /// <response code="204">Signal light always started with default color.</response>
        /// <param name="defaultLight">New default color value.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/api/[controller]/parameters/{defaultLight}")]
        public IActionResult UpdateDefaultLight(LightColor defaultLight)
        {
            _configurationService.UpdateDefaultLight(defaultLight);
            return NoContent();
        }
    }
}