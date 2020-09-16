using System.Collections.Generic;

namespace SeoulAir.Device.Domain.Interfaces.HelperClasses
{
    public interface IRowConverter<TDto>
    {
        TDto ConvertRow(ICollection<string> columns);
    }
}
