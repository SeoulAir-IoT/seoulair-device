namespace SeoulAir.Device.Domain.Resources
{
    public static class Strings
    {
        public const string FileDoesNotExistMessage =
            "Specified file does not exist or does not match required extension.";
        public const string InvalidFileFormatMessage =
            "Specified file does not match format that is required for the device.";
        public const string InvalidDateFormatMessage = "Application does not support provided date format.";
        public const string InvalidStationCodeMessage = "Application does not support provided station code format";
        public const string InvalidColumnTypeMessage = "Column {0} has unexpected data type.";

        #region Exception Middleware Handler

        public const string InternalServerErrorUri = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        public const string ConflictUri = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
        public const string InternalServerErrorTitle = "500 Internal Server Error.";
        public const string ConflictTitle = "409 Conflict.";

        #endregion

        #region Swagger Documentation

        public const string OpenApiInfoProjectName = "SeoulAir.Device API";
        public const string OpenApiInfoTitle = "SeoulAir Device microservice.";
        public const string OpenApiInfoProjectVersion = "1.0";
        public const string OpenApiInfoDescription
            = "SeoulAir Device is microservice that is part of SeoulAir project.\n" +
            "For more information visit Gitlab Repository";
        public const string SwaggerEndpoint = "/swagger/{0}/swagger.json";
        public const string GitlabContactName = "Gitlab Repository";
        public const string GitlabRepoUri = "http://gitlab.com/seoulair/seoulair-device.git";

        #endregion

        public const string ChangeColorExceptionMessage =
            "Impossible to change color while signal light device is off or if it does not exist.";
        public const string ParameterNullOrEmptyMessage = "Parameter {0} must not be null or empty string.";
        public const string ParameterBetweenMessage = "Value of parameter {0} must be between {1} and {2}.";
        public const string StationCodeDoesNotExistMessage = "Station code {0} unknown.";
    }
}
