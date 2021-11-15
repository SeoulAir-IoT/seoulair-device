using Microsoft.AspNetCore.Mvc;
using SeoulAir.Device.Client;

namespace SeoulAir.Device.Api.Controllers;

[Route(IAirQualitySensorApi.ApiBase)]
[ApiController]
public class AirQualitySensorController : ControllerBase, IAirQualitySensorApi
{
    public AirQualitySensorController()
    {
    }

    public int GetNameOfSensor()
    {
        throw new System.NotImplementedException();
    }

    public int GetSendingDelayInMs()
    {
        throw new System.NotImplementedException();
    }

    public int GetStationCode()
    {
        throw new System.NotImplementedException();
    }

    public bool IsOn()
    {
        throw new System.NotImplementedException();
    }

    public void TurnOff()
    {
        throw new System.NotImplementedException();
    }

    public void TurnOn()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateDelay(int delayMs)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateNameOfSensor(string newName)
    {
        throw new System.NotImplementedException();
    }
}
