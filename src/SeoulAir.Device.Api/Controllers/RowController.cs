using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Domain.Interfaces.Services;
using System;

namespace SeoulAir.Device.Api.Controllers;

[Route("api/[controller]/[action]")]
public class RowController : ControllerBase
{

    public RowController(IDataService service)
    {
    }

    [HttpGet]
    public IActionResult Next()
    {
        throw new NotImplementedException();
    }
}
