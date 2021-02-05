using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Exceptions;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Domain.Services.HelperClasses
{
    public class RawDataInstanceRowConverter : IRowConverter<RawDataInstanceDto>
    {
        public RawDataInstanceDto ConvertRow(ICollection<string> columns)
        {
            if (columns == null) 
                throw new ArgumentNullException(nameof(columns));
            if (columns.Count != 15)
                throw new InvalidFileFormatException(InvalidFileFormatMessage);

            RawDataInstanceDto result = new RawDataInstanceDto();

            if (!DateTime.TryParse(columns.First(), out var tempHolder))
                throw new InvalidDateTimeFormatException(InvalidDateFormatMessage);
            result.MeasurementDate = tempHolder;

            PopulateStationInfo(result, columns.ToList());

            PopulateAirPollutionInfo(result, columns.ToList());

            return result;
        }

        private void PopulateStationInfo(RawDataInstanceDto result, List<string> columns)
        {
            result.StationInfo = new StationInfoDto();

            if (!ushort.TryParse(columns[1], out var shortHolder))
                throw new InvalidStationCodeFormatException(InvalidStationCodeMessage);

            result.StationInfo.StationCode = shortHolder;

            result.StationInfo.StationAddress = string.Join(",", columns.ToArray(), 2, 5).Trim('\"');

            if (!double.TryParse(columns[7], out var doubleHolder))
                throw new InvalidColumnTypeException(
                    string.Format(InvalidColumnTypeMessage, nameof(result.StationInfo.Latitude)));

            result.StationInfo.Latitude = doubleHolder;

            if (!double.TryParse(columns[8], out doubleHolder))
                throw new InvalidColumnTypeException(
                    string.Format(InvalidColumnTypeMessage, nameof(result.StationInfo.Longitude)));

            result.StationInfo.Longitude = doubleHolder;
        }

        private void PopulateAirPollutionInfo(RawDataInstanceDto result, List<string> columns)
        {
            result.AirPollutionInfo = new AirPollutionInfoDto();

            if (!double.TryParse(columns[9], out var tempHolder))
                throw new InvalidColumnTypeException(
                    string.Format(InvalidColumnTypeMessage, nameof(result.AirPollutionInfo.So2)));

            result.AirPollutionInfo.So2 = tempHolder;

            if (!double.TryParse(columns[10], out tempHolder))
                throw new InvalidColumnTypeException(
                    string.Format(InvalidColumnTypeMessage, nameof(result.AirPollutionInfo.No2)));

            result.AirPollutionInfo.No2 = tempHolder;

            if (!double.TryParse(columns[11], out tempHolder))
                throw new InvalidColumnTypeException(
                    string.Format(InvalidColumnTypeMessage, nameof(result.AirPollutionInfo.O3)));

            result.AirPollutionInfo.O3 = tempHolder;

            if (!double.TryParse(columns[12], out tempHolder))
                throw new InvalidColumnTypeException(
                    string.Format(InvalidColumnTypeMessage, nameof(result.AirPollutionInfo.Co)));

            result.AirPollutionInfo.Co = tempHolder;

            if (!double.TryParse(columns[13], out tempHolder))
                throw new InvalidColumnTypeException(string.Format(InvalidColumnTypeMessage, nameof(result.AirPollutionInfo.Pm10)));

            result.AirPollutionInfo.Pm10 = tempHolder;

            if (!double.TryParse(columns[14], out tempHolder))
                throw new InvalidColumnTypeException(string.Format(InvalidColumnTypeMessage, nameof(result.AirPollutionInfo.Pm25)));

            result.AirPollutionInfo.Pm25 = tempHolder;
        }
    }
}
