﻿using SeoulAir.Device.Domain.Exceptions;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using System.IO;
using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Options;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Domain.Services.HelperClasses
{
    public sealed class CsvReader<TDto> : ICsvReader<TDto>
        where TDto : class
    {
        private readonly string _dataPath;
        private StreamReader _fileReader;
        private readonly IRowConverter<TDto> _rowConverter;

        public CsvReader(IOptions<AirQualitySensorOptions> settings, IRowConverter<TDto> rowConverter)
        {
            _dataPath = settings.Value.DataPath;
            _rowConverter = rowConverter;
        }

        public void CloseFile()
        {
            if (_fileReader == null)
                return;

            _fileReader.Close();
            _fileReader.Dispose();
            _fileReader = null;
        }

        public void ReopenFile()
        {
            if (_fileReader != null)
            {
                _fileReader.Close();
                _fileReader.Dispose();
                _fileReader = null;
            }

            if (!File.Exists(_dataPath))
                throw new FileDoesNotExistException(FileDoesNotExistMessage);

            _fileReader = new StreamReader(_dataPath);
            _fileReader.ReadLine();
        }

        public void OpenFile()
        {
            if (_fileReader != null)
                return;

            if (!File.Exists(_dataPath))
                throw new FileDoesNotExistException(FileDoesNotExistMessage);

            _fileReader = new StreamReader(_dataPath);
            _fileReader.ReadLine();
        }

        public void Dispose()
        {
            _fileReader.Close();
            _fileReader.Dispose();
        }

        public bool TryReadNextRow(out TDto result)
        {
            if (_fileReader.EndOfStream)
            {
                result = null;
                return false;
            }

            var singleRow = _fileReader.ReadLine();
            result = _rowConverter.ConvertRow(singleRow?.Split(','));
            
            return true;
        }
    }
}