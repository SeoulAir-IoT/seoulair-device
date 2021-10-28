using Refit;
using SeoulAir.Device.Client.Enums;

namespace SeoulAir.Device.Client
{
    public interface ISignalLightApi
    {
        protected const string ApiBase = "api/signalLights";

        protected const string GetActiveColorPath = ApiBase + "/{stationCode}/color";
        /// <summary> Returns the color of active light for specified station code. </summary>
        [Get(GetActiveColorPath)]
        public SignalLightColors GetActiveColor(string stationCode);

        protected const string ChangeActiveColorPath = ApiBase + "{stationCode}/color";
        /// <summary>Updates active color of signal light for specified station code.</summary>
        /// <param name="stationCode">Station code of the light that needs to be changed</param>
        /// <param name="color">New color value. Color value is LightColor enum.</param>
        [Put(ChangeActiveColorPath)]
        public void ChangeActiveColor(string stationCode, [Body] SignalLightColors color);

        protected const string IsOnPath = ApiBase + "/{stationCode}/isOn";
        /// <summary> Checks if signal light is on for the specific station.</summary>
        [Get(IsOnPath)]
        public bool IsOn(string stationCode);

        protected const string TurnOnPath = ApiBase + "/{stationCode}/turnOn";
        /// <summary>Turns on the signal light for specified station code.</summary>
        /// <remarks>Signal light is started with default color that is configured in appsettings.json</remarks>
        [Put(TurnOnPath)]
        public void TurnOn(string stationCode);
        
        protected const string TurnOffPath = ApiBase + "/{stationCode}/turnOff";
        /// <summary>Turns off the signal light for specified station code.</summary>
        /// <remarks>Current color or signal light is not saved. Color will be default one at next start</remarks>
        [Put(TurnOffPath)]
        public void TurnOff(string stationCode);

        protected const string GetConfigurationPath = ApiBase + "/{stationCode}/turnOff";
        /// <summary> Returns active configuration on witch application is running. </summary>
        [Get(GetConfigurationPath)]
        public void GetConfiguration(string stationCode);

        protected const string UpdateNamePath = ApiBase + "/{stationCode}/configuration/name";
        /// <summary>Updates the name of signal light. </summary>
        /// <param name="newName">New name.</param>
        /// <param name="stationCode">Station code of the light that needs to be changed</param>
        [Put(UpdateNamePath)]
        public void UpdateName(string stationCode, [Body] string newName);

        protected const string UpdateDefaultColorPath = ApiBase + "/{stationCode}/configuration/defaultColor";
        /// <summary>Updates default color of signal light. </summary>
        /// <param name="newDefaultColor">New default color value.</param>
        /// <param name="stationCode">Station code of the light that needs to be changed</param>
        [Put(UpdateDefaultColorPath)]
        public void UpdateDefaultColor(string stationCode, [Body] SignalLightColors newDefaultColor);
    }
}