using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Api.ViewModels;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;
using System;

namespace SeoulAir.Device.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        private readonly IConfigurationService _service;
        private readonly IMapper _mapper;

        public ParametersController(IConfigurationService service, IMapper mapper) : base()
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            CurrentConfigurationModel result;
            try
            {
                result = _mapper.Map<CurrentConfigurationModel>(_service.GetCurrentParameters());
                result.SendingTreshold += " ms";
            }
            catch(Exception exception)
            {
                return NotFound(exception);
            }
            return Ok(result);
        }

        [HttpPut("treshold")]
        public IActionResult UpdateTimeTrashold(uint newTreshould)
        {
            try
            {
                _service.Update(newTreshould);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
            return NoContent();
        }

        [HttpPut("deviceType")]
        public IActionResult Put(string newDeviceType)
        {
            try
            {
                _service.Update(newDeviceType);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
            return NoContent();
        }

        [HttpPut()]
        public IActionResult Put(CurrentConfigurationModel newConfiguration)
        {
            try
            {
                _service.Update(_mapper.Map<DeviceSettings>(newConfiguration));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
            return NoContent();
        }
    }
}