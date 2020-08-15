using System;
using System.Threading.Tasks;

namespace SeoulAir.Device.Domain.Interfaces.Services
{
    public interface IMqttService<in TDto> : IDisposable
        where TDto : class
    {
        Task SendDto(TDto messageObject);

        Task OpenConnection();

        Task CloseConnection();
    }
}
