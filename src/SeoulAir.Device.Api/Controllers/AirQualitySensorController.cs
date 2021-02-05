using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Options;

namespace SeoulAir.Device.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AirQualitySensorController : Controller
    {
        private readonly IDataService _service;
        private readonly IAirQualitySensorConfigurationService _configurationService;

        public AirQualitySensorController(IDataService service,
            IAirQualitySensorConfigurationService configurationService)
        {
            _service = service;
            _configurationService = configurationService;
        }

        /// <summary>Turns on the sensor and starts measuring data. </summary>
        /// <remarks>Sensor is connected to Mqtt Broker from appsettings.json.
        /// All measured data is sent through configured topic.</remarks>
        /// <response code="204">Sensor started</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public IActionResult TurnOn()
        {
            _service.StartDevice();
            return NoContent();
        }

        /// <summary>Turns off the sensor. Data is no longer sent through the topic. </summary>
        /// <response code="204">Sensor not working anymore</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public IActionResult TurnOff()
        {
            _service.StopDevice();
            return NoContent();
        }

        /// <summary> Checks if sensor is on. (measuring data and sending it to mqtt broker) </summary>
        /// <response code="200">Operation completed successfully and boolean result is returned</response>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<bool> IsOn()
        {
            return Ok(_service.IsOn);
        }
        
        /// <summary> Returns last measured instance of data. Saved only temporarily. </summary>
        /// <response code="200">Data fetched successfully and returned.</response>
        [ProducesResponseType(typeof(RawDataInstanceDto), StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<RawDataInstanceDto> LastMeasuredData()
        {
            return Ok(_service.LastReadData);
        }

        /// <summary> Returns active configuration on which application is running. </summary>
        /// <response code="200">Configuration fetched successfully and returned.</response>
        [ProducesResponseType(typeof(AirQualitySensorOptions), StatusCodes.Status200OK)]
        [HttpGet("/api/[controller]/parameters")]
        public ActionResult<AirQualitySensorOptions> GetActiveParameters()
        {
            return Ok(_configurationService.ActiveConfiguration);
        }
        
        /// <summary>Updates Sensor configuration with changing sensor name. </summary>
        /// <response code="204">Name changed successfully.</response>
        /// <param name="name">New name for the sensor.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/api/[controller]/parameters/name/{name:minlength(2)}")]
        public IActionResult UpdateName(string name)
        {
            _configurationService.UpdateSensorName(name);
            return NoContent();
        }
        
        /// <summary>Updates Sensor configuration with changing sendingDelayMs (configuration). </summary>
        /// <response code="204">Delay is changed to new value.</response>
        /// <param name="sendingDelayMs">New value for sendingDelayMs. Value is represented in milliseconds.</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("/api/[controller]/parameters/sendingDelayMs/{sendingDelayMs:min(1)}")]
        public IActionResult UpdateDefaultLight(int sendingDelayMs)
        {
            _configurationService.UpdateReadingDelay((ushort)sendingDelayMs);
            return NoContent();
        }
    }
}