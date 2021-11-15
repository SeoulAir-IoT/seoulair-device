using Microsoft.AspNetCore.Mvc;
using Refit;
using SeoulAir.Device.Client;
using SeoulAir.Device.Client.Enums;
using System;

namespace SeoulAir.Device.Api.Controllers;

[ApiController]
[Route(ISignalLightApi.ApiBase)]
public class SignalLightController : ControllerBase, ISignalLightApi
{
    public SignalLightController()
    {
    }

    public void ChangeActiveColor(string stationCode, [Body] SignalLightColors color)
    {
        throw new NotImplementedException();
    }

    public SignalLightColors GetActiveColor(string stationCode)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string stationCode)
    {
        throw new NotImplementedException();
    }

    public void TurnOff(string stationCode)
    {
        throw new NotImplementedException();
    }

    public void TurnOn(string stationCode)
    {
        throw new NotImplementedException();
    }
}
