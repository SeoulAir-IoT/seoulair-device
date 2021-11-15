using System.Collections.Generic;

namespace SeoulAir.Device.Domain.Interfaces.Utility;

public interface IRowConverter<TDto>
{
    TDto ConvertRow(ICollection<string> columns);
}
