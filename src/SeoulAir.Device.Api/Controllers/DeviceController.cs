using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Domain.Interfaces.Services;

namespace SeoulAir.Device.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDataService _service;

        public DeviceController(IDataService service)
        {
            _service = service;
        }

        [HttpPut]
        public IActionResult Start()
        {
            _service.StartDevice();
            return Ok();
        }

        [HttpPut]
        public IActionResult Stop()
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
    }
}
