using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;
using System;

namespace SeoulAir.Device.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RowController : ControllerBase
    {
        private readonly IDataService _service;

        public RowController(IDataService service) : base()
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Next()
        {
            RawDataInstanceDto result;
            try
            {
                result = _service.ReadNext();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
