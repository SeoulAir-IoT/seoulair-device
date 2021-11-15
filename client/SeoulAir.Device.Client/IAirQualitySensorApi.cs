using Refit;

namespace SeoulAir.Device.Client;

public interface IAirQualitySensorApi
{
    protected const string ApiBase = "api/airQualitySensor";

    protected const string GetIsOnPath = "/isOn";
    /// <summary>
    /// Checks if sensor is working and reading new data or not.
    /// </summary>
    [Get(GetIsOnPath)]
    public bool IsOn();

    protected const string TurnOffPath = "/turnOff";
    /// <summary>
    /// Turns of the sensor, after this no new data will be sent.
    /// </summary>
    [Put(TurnOffPath)]
    public void TurnOff();

    protected const string TurnOnPath = "/turnOn";
    /// <summary>
    /// Turns on the sensor, data will start to be sent after this.
    /// </summary>
    [Put(TurnOnPath)]
    public void TurnOn();

    protected const string GetDelayConfigurationPath = "/configuration/delayMs";
    /// <summary>
    /// Gets sensor reading delay in ms 
    /// </summary>
    [Get(GetDelayConfigurationPath)]
    public int GetSendingDelayInMs();

    protected const string UpdateDelayConfigurationPath = "/configuration/delayMs";
    /// <summary>
    /// Updates sensor so it sends data based on provided parameter 
    /// </summary>
    /// <param name="delayMs">new delay in miliseconds</param>
    [Put(UpdateDelayConfigurationPath)]
    public void UpdateDelay(int delayMs);

    protected const string GetNameOfSensorPath = "/configuration/name";
    /// <summary>
    /// Gets the name of sensor
    /// </summary>
    [Get(GetNameOfSensorPath)]
    public int GetNameOfSensor();

    protected const string UpdateNameOfSensorPath = "/configuration/name";
    /// <summary>
    /// Updates the name identifier of the sensor
    /// </summary>
    [Put(UpdateNameOfSensorPath)]
    public void UpdateNameOfSensor(string newName);

    protected const string GetStationCodePath = "/configuration/stationCode";
    /// <summary>
    /// Gets station code identifier from the sensor
    /// </summary>
    [Get(GetStationCodePath)]
    public int GetStationCode();
}
