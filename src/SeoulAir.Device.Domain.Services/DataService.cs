using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.Services;
using SeoulAir.Device.Domain.Interfaces.Utility;
using SeoulAir.Device.Domain.Options;
using System.Threading.Tasks;

namespace SeoulAir.Device.Domain.Services;

public class DataService : IDataService
{
    private readonly SeoulAirDeviceOptions _settings;
    private readonly ICsvReader<RawDataInstanceDto> _dataReader;
    private readonly IMqttService<RawDataInstanceDto> _mqttService;
    private Task _currentTask;

    public RawDataInstanceDto LastReadData { get; private set; }

    private readonly object _isOnLocker = new object();
    private bool _isOn;
    public bool IsOn
    {
        get
        {
            lock (_isOnLocker)
            {
                return _isOn;
            }
        }
        private set
        {
            lock (_isOnLocker)
            {
                _isOn = value;
            }
        }
    }

    public DataService(IOptions<SeoulAirDeviceOptions> settings,
                       ICsvReader<RawDataInstanceDto> dataReader,
                       IMqttService<RawDataInstanceDto> mqttService)
    {
        _settings = settings.Value;
        _mqttService = mqttService;
        _dataReader = dataReader;
        _isOn = false;
        LastReadData = null;
        _currentTask = null;
    }

    public RawDataInstanceDto ReadNext()
    {
        _dataReader.OpenFile();
        _dataReader.TryReadNextRow(out var result);

        return result;
    }

    public void StopDevice()
    {
        IsOn = false;
        _currentTask.Wait();
    }

    public void StartDevice()
    {
        if (IsOn)
            return;
        IsOn = true;
        _currentTask = Task.Run(SensorTaskAsync);
    }

    private async Task SensorTaskAsync()
    {
        _dataReader.OpenFile();
        await _mqttService.OpenConnection();
        while (IsOn)
        {
            await Task.Delay((int)_settings.ReadingDelayMs);
            if (!_dataReader.TryReadNextRow(out _))
                _dataReader.ReopenFile();

            _dataReader.TryReadNextRow(out var result);
            LastReadData = result;

            await _mqttService.SendDto(result);
        }
        await _mqttService.CloseConnection();
        _dataReader.CloseFile();
    }
}
