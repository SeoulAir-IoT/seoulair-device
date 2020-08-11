using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using SeoulAir.Device.Domain.Interfaces.Services;

namespace SeoulAir.Device.Domain.Services
{
    public class DataService : IDataService
    {
        private readonly DeviceSettings _settings;
        private readonly ICsvReader<RawDataInstanceDto> _dataReader;

        public DataService(AppSettings settings, ICsvReader<RawDataInstanceDto> dataReader)
        {
            _settings = settings.DeviceSettings;
            _dataReader = dataReader;
        }

        public RawDataInstanceDto ReadNext()
        {
            RawDataInstanceDto result = null;
            _dataReader.OpenFile();
            _dataReader.TryReadNextRow(out result);
            return result;
        }
    }
}
