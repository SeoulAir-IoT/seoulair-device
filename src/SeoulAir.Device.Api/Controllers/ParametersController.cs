using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Api.ViewModels;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;

namespace SeoulAir.Device.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        private readonly IConfigurationService _service;
        private readonly IMapper _mapper;

        public ParametersController(IConfigurationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            CurrentConfigurationModel result;
            result = _mapper.Map<CurrentConfigurationModel>(_service.GetCurrentParameters());
            result.ReadingDelayMs += " ms";

            return Ok(result);
        }

        [HttpPut("treshold")]
        public IActionResult UpdateTimeTrashold(uint newTreshould)
        {
            _service.Update(newTreshould);

            return NoContent();
        }

        [HttpPut("deviceType")]
        public IActionResult Put(string newDeviceType)
        {
            _service.Update(newDeviceType);

            return NoContent();
        }

        [HttpPut]
        public IActionResult Put(CurrentConfigurationModel newConfiguration)
        {
            _service.Update(_mapper.Map<DeviceSettings>(newConfiguration));

            return NoContent();
        }
    }
}