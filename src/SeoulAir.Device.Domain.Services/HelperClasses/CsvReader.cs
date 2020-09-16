using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Exceptions;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using System;
using System.IO;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Domain.Services.HelperClasses
{
    public sealed class CsvReader<TDto> : ICsvReader<TDto>
        where TDto : class
    {
        private readonly string _dataPath;
        private StreamReader fileReader;
        private readonly IRowConverter<TDto> _rowConverter;

        public CsvReader(AppSettings settings, IRowConverter<TDto> rowConverter)
        {
            _dataPath = settings.DeviceSettings.DataPath;
            _rowConverter = rowConverter;
        }

        public void CloseFile()
        {
            if (fileReader == null)
                return;

            fileReader.Close();
            fileReader.Dispose();
            fileReader = null;
        }

        public void ReopenFile()
        {
            if (fileReader != null)
            {
                fileReader.Close();
                fileReader.Dispose();
                fileReader = null;
            }

            if (!File.Exists(_dataPath))
                throw new FileDoesNotExistException(FileDoesNotExistMessage);

            fileReader = new StreamReader(_dataPath);
            fileReader.ReadLine();
        }

        public void OpenFile()
        {
            if (fileReader != null)
                return;

            if (!File.Exists(_dataPath))
                throw new FileDoesNotExistException(FileDoesNotExistMessage);

            fileReader = new StreamReader(_dataPath);
            fileReader.ReadLine();
        }

        public void Dispose()
        {
            fileReader.Close();
            fileReader.Dispose();
        }

        public bool TryReadNextRow(out TDto result)
        {
            if (fileReader.EndOfStream)
            {
                result = null;
                return false;
            }

            string singleRow = fileReader.ReadLine();
            result = _rowConverter.ConvertRow(singleRow.Split(','));
            return true;
        }
    }
}
