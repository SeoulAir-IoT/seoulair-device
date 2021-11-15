using SeoulAir.Device.Domain.Enums;
using System;
using System.Runtime.Serialization;

namespace SeoulAir.Device.Domain.Exceptions;

[Serializable]
public class InternalSeverErrorException : SeoulAirException
{
    public override string Title => "Something went wrong!";

    public InternalSeverErrorException(SeoulAirDeviceErrorCodes code) : base(500, code) { }

    public InternalSeverErrorException(SeoulAirDeviceErrorCodes code, string message) : base(500, code, message) { }

    public InternalSeverErrorException(SeoulAirDeviceErrorCodes code, string message, Exception innerException)
        : base(500, code, message, innerException) { }

    protected InternalSeverErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
