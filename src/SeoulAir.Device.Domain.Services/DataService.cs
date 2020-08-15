﻿using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using SeoulAir.Device.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace SeoulAir.Device.Domain.Services
{
    public class DataService : IDataService
    {
        private readonly DeviceSettings _settings;
        private readonly ICsvReader<RawDataInstanceDto> _dataReader;
        private readonly IMqttService<RawDataInstanceDto> _mqttService;
        private Task CurrentTask;

        public RawDataInstanceDto LastReadData { get; private set; }

        private readonly object IsOnLocker = new object();
        private bool _isOn;
        public bool IsOn
        {
            get
            {
                lock (IsOnLocker)
                {
                    return _isOn;
                }
            }
            private set
            {
                lock (IsOnLocker)
                {
                    _isOn = value;
                }
            }
        }

        public DataService(AppSettings settings,
                           ICsvReader<RawDataInstanceDto> dataReader,
                           IMqttService<RawDataInstanceDto> mqttService)
        {
            _settings = settings.DeviceSettings;
            _mqttService = mqttService;
            _dataReader = dataReader;
            _isOn = false;
            LastReadData = null;
            CurrentTask = null;
        }

        public RawDataInstanceDto ReadNext()
        {
            RawDataInstanceDto result = null;
            _dataReader.OpenFile();
            _dataReader.TryReadNextRow(out result);
            return result;
        }

        public void StopDevice()
        {
            IsOn = false;
            CurrentTask.Wait();
        }

        public void StartDevice()
        {
            IsOn = true;
            CurrentTask = Task.Run(SensorTaskAsync);
        }

        private async Task SensorTaskAsync()
        {
            _dataReader.OpenFile();
            await _mqttService.OpenConnection();
            while (IsOn)
            {
                await Task.Delay((int)_settings.SendingTreshold);
                RawDataInstanceDto result = null;

                if (!_dataReader.TryReadNextRow(out result))
                    _dataReader.ReopenFile();

                _dataReader.TryReadNextRow(out result);
                LastReadData = result;

                await _mqttService.SendDto(result);
            }
            await _mqttService.CloseConnection();
            _dataReader.CloseFile();
        }
    }
}