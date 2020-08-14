using SeoulAir.Device.Domain.Dtos;

namespace SeoulAir.Device.Domain.Interfaces.Services
{
    public interface IDataService
    {
        RawDataInstanceDto ReadNext();

        void StopDevice();

        void StartDevice();

        bool IsOn { get; }

        RawDataInstanceDto LastReadData { get; }
    }
}
