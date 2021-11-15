using SeoulAir.Device.Domain.Dtos;

namespace SeoulAir.Device.Domain.Interfaces.Services;

public interface IDataService
{
    bool IsOn { get; }

    RawDataInstanceDto LastReadData { get; }

    RawDataInstanceDto ReadNext();

    void StopDevice();

    void StartDevice();
}
