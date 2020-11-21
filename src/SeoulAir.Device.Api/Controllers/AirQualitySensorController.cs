using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Domain.Interfaces.Services;

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

        [HttpPut]
        public IActionResult TurnOn()
        {
            _service.StartDevice();
            return Ok();
        }

        [HttpPut]
        public IActionResult TurnOff()
        {
            _service.StopDevice();
            return Ok();
        }

        [HttpGet]
        public IActionResult IsOn()
        {
            return Ok(_service.IsOn);
        }
        
        [HttpGet]
        public IActionResult LastData()
        {
            return Ok(_service.LastReadData);
        }

        [HttpGet("/api/[controller]/parameters")]
        public IActionResult GetActiveParameters()
        {
            return Ok(_configurationService.ActiveConfiguration);
        }
        
        [HttpPut("/api/[controller]/parameters/{name}")]
        public IActionResult UpdateName(string name)
        {
            _configurationService.UpdateSensorName(name);
            return Ok();
        }
        
        [HttpPut("/api/[controller]/parameters/{sendingDelayMs}")]
        public IActionResult UpdateDefaultLight(int sendingDelayMs)
        {
            _configurationService.UpdateReadingDelay((ushort)sendingDelayMs);
            return Ok();
        }
    } 
}