using SeoulAir.Device.Domain.Dtos;

namespace SeoulAir.Device.Domain.Interfaces.Services
{
    public interface IDataService
    {
        RawDataInstanceDto ReadNext();
    }
}
