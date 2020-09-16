using System;

namespace SeoulAir.Device.Domain.Interfaces.HelperClasses
{
    public interface ICsvReader<TDto> : IDisposable
    {
        void OpenFile();

        void ReopenFile();

        void CloseFile();

        bool TryReadNextRow(out TDto result);
    }
}
