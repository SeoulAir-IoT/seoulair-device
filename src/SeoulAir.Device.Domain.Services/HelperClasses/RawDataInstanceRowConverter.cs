using SeoulAir.Device.Domain.Dtos;
using SeoulAir.Device.Domain.Interfaces.HelperClasses;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SeoulAir.Device.Domain.Services.HelperClasses
{
    public class RawDataInstanceRowConverter : IRowConverter<RawDataInstanceDto>
    {
        //TODO: implement error handling
        public RawDataInstanceDto ConvertRow(IEnumerable<string> columns)
        {
            if (columns.Count() != 14)
                throw new Exception();

            RawDataInstanceDto result = new RawDataInstanceDto();

            if (!DateTime.TryParse(columns.First(), out var tempHolder))
                throw new Exception();
            result.MeasurementDate = tempHolder;

            PopulateStationInfo(result, columns.ToList());

            PopulateAirPollutionInfo(result, columns.ToList());

            return result;
        }

        private void PopulateStationInfo(RawDataInstanceDto result, List<string> columns)
        {
            result.StationInfo = new StationInfoDto();

            if (!ushort.TryParse(columns[1], out var shortHolder))
                throw new Exception();

            result.StationInfo.StationCode = shortHolder;

            result.StationInfo.StationAddress = string.Join(",", columns, 2, 5);

            if (!double.TryParse(columns[7], out var doubleHolder))
                throw new Exception();

            result.StationInfo.Latitude = doubleHolder;

            if (!double.TryParse(columns[8], out doubleHolder))
                throw new Exception();

            result.StationInfo.Longitude = doubleHolder;
        }

        private void PopulateAirPollutionInfo(RawDataInstanceDto result, List<string> columns)
        {
            result.AirPollutionInfo = new AirPollutionInfoDto();

            if (!double.TryParse(columns[9], out var tempHolder))
                throw new Exception();

            result.AirPollutionInfo.So2 = tempHolder;

            if (!double.TryParse(columns[10], out tempHolder))
                throw new Exception();

            result.AirPollutionInfo.No2 = tempHolder;

            if (!double.TryParse(columns[11], out tempHolder))
                throw new Exception();

            result.AirPollutionInfo.O3 = tempHolder;

            if (!double.TryParse(columns[12], out tempHolder))
                throw new Exception();

            result.AirPollutionInfo.Co = tempHolder;

            if (!double.TryParse(columns[13], out tempHolder))
                throw new Exception();

            result.AirPollutionInfo.Pm10 = tempHolder;

            if (!double.TryParse(columns[14], out tempHolder))
                throw new Exception();

            result.AirPollutionInfo.Pm25 = tempHolder;
        }
    }
}
