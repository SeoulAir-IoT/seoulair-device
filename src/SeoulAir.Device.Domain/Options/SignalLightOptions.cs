using System.Collections.Generic;
using SeoulAir.Device.Domain.Enums;

namespace SeoulAir.Device.Domain.Options
{
    public class SignalLightOptions
    {
        public static string AppSettingsPath { get; } = "SignalLight";
        
        public string Name { get; set; }
        public List<string> StationCodes { get; set; }
        public LightColor DefaultColor { get; set; }
    }
}
