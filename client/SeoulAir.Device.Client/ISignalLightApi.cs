using Refit;
using SeoulAir.Device.Client.Enums;

namespace SeoulAir.Device.Client;

public interface ISignalLightApi
{
    protected const string ApiBase = "api/signalLights";

    protected const string GetActiveColorPath = ApiBase + "/color";
    /// <summary> Returns the color of active light for specified station code. </summary>
    [Get(GetActiveColorPath)]
    public SignalLightColors GetActiveColor(string stationCode);

    protected const string ChangeActiveColorPath = ApiBase + "/color";
    /// <summary>Updates active color of signal light for specified station code.</summary>
    /// <param name="stationCode">Station code of the light that needs to be changed</param>
    /// <param name="color">New color value. Color value is LightColor enum.</param>
    [Put(ChangeActiveColorPath)]
    public void ChangeActiveColor(string stationCode, [Body] SignalLightColors color);

    protected const string IsOnPath = ApiBase + "/isOn";
    /// <summary> Checks if signal light is on for the specific station.</summary>
    [Get(IsOnPath)]
    public bool IsOn(string stationCode);

    protected const string TurnOnPath = ApiBase + "/turnOn";
    /// <summary>Turns on the signal light for specified station code.</summary>
    /// <remarks>Signal light is started with default color that is configured in appsettings.json</remarks>
    [Put(TurnOnPath)]
    public void TurnOn(string stationCode);

    protected const string TurnOffPath = ApiBase + "/turnOff";
    /// <summary>Turns off the signal light for specified station code.</summary>
    /// <remarks>Current color or signal light is not saved. Color will be default one at next start</remarks>
    [Put(TurnOffPath)]
    public void TurnOff(string stationCode);
}
