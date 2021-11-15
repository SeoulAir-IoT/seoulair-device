using Microsoft.Extensions.Options;
using SeoulAir.Device.Domain.Options;
using System.Collections.Generic;
using System.Linq;
using static SeoulAir.Device.Domain.Resources.Strings;

namespace SeoulAir.Device.Api.Configuration.Validators;

public class SeoulAirDeviceOptionsValidator : IValidateOptions<SeoulAirDeviceOptions>
{
    public ValidateOptionsResult Validate(string name, SeoulAirDeviceOptions options)
    {
        ICollection<string> failureMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(options.Name))
            failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.Name)));

        //TODO: Standardize the message
        if (string.IsNullOrWhiteSpace(options.StationCode)
            || !int.TryParse(options.StationCode, out var temp)
            || temp < 0)
            failureMessages.Add("Station code is either invalid or empty.");

        if (string.IsNullOrWhiteSpace(options.DataPath))
            failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.DataPath)));

        //TODO: Standardize the message
        if (string.IsNullOrWhiteSpace(options.ReadingDelayMs)
            || !int.TryParse(options.ReadingDelayMs, out var tempDelay)
            || tempDelay < 0)
            failureMessages.Add("Delay is either invalid or empty.");

        return failureMessages.Any()
            ? ValidateOptionsResult.Fail(failureMessages)
            : ValidateOptionsResult.Success;
    }
}
