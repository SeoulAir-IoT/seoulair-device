using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Options;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Domain.Services.OptionsValidators
{
    public class AirQualitySensorOptionsValidator : IValidateOptions<AirQualitySensorOptions>
    {
        public ValidateOptionsResult Validate(string name, AirQualitySensorOptions options)
        {
            ICollection<string> failureMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(options.Name))
                failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.Name)));
            
            if (string.IsNullOrWhiteSpace(options.DataPath))
                failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.Name)));

            return failureMessages.Any()
                ? ValidateOptionsResult.Fail(failureMessages)
                : ValidateOptionsResult.Success;
        }
    }
}
