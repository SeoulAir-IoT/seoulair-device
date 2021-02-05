using SeoulAir.Device.Domain.Enums;

namespace SeoulAir.Device.Domain.Dtos
{
    public class StationSignalLight
    {
        public bool IsOn { get; set; }
        public string StationCode { get; set; }
        public LightColor ActiveColor { get; set; }
    }
}
