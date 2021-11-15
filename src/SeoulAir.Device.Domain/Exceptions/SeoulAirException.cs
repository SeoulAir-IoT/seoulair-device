using SeoulAir.Device.Domain.Enums;
using System;
using System.Runtime.Serialization;

namespace SeoulAir.Device.Domain.Exceptions;

[Serializable]
public abstract class SeoulAirException : Exception
{
    public int StatusCode { get; }
    public SeoulAirDeviceErrorCodes ErrorCode { get; }
    public abstract string Title { get; }

    protected SeoulAirException(int statusCode, SeoulAirDeviceErrorCodes code) : base() 
    {
        StatusCode = statusCode;
        ErrorCode = code;
    }

    protected SeoulAirException(int statusCode, SeoulAirDeviceErrorCodes code, string message) : base(message) 
    {
        StatusCode = statusCode;
        ErrorCode = code;
    }

    protected SeoulAirException(int statusCode, SeoulAirDeviceErrorCodes code, string message, Exception innerException)
        : base(message, innerException) 
    {
        StatusCode = statusCode;
        ErrorCode = code;
    }

    protected SeoulAirException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
