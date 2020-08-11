using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using System;
using System.IO;

namespace SeoulAir.Device.Domain.Services.HelperClasses
{
    public class CsvReader<TDto> : ICsvReader<TDto>
        where TDto : class
    {
        private readonly string _dataPath;
        private readonly IRowConverter<TDto> _rowConverter;
        private StreamReader fileReader;

        //TODO: implement configuration
        //TODO: implement exceptions
        //TODO: Inspect sonarLint message
        public CsvReader(string dataPath, IRowConverter<TDto> rowConverter)
        {
            _dataPath = dataPath;
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

        public void Dispose()
        {
            fileReader.Close();
            fileReader.Dispose();
        }

        public void OpenFile()
        {
            if(fileReader != null)
            {
                fileReader.Close();
                fileReader.Dispose();
                fileReader = null;
            }

            if (!File.Exists(_dataPath))
                throw new Exception();

            fileReader = new StreamReader(_dataPath);
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
