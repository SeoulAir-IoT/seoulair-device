using SeoulAir.Device.Domain.Enums;
using System;
using System.Runtime.Serialization;

namespace SeoulAir.Device.Domain.Exceptions;

[Serializable]
public class BadRequestException : SeoulAirException
{
    public override string Title => "Bad request!";

    public BadRequestException(SeoulAirDeviceErrorCodes code) : base(500, code) { }

    public BadRequestException(SeoulAirDeviceErrorCodes code, string message) : base(500, code, message) { }

    public BadRequestException(SeoulAirDeviceErrorCodes code, string message, Exception innerException)
        : base(500, code, message, innerException) { }

    protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
