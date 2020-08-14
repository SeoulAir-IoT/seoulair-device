using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Domain.Interfaces.Services;

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
            return Ok(_service.ReadNext());
        }
    }
}
