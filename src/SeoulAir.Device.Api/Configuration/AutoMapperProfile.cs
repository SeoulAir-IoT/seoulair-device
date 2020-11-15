using AutoMapper;
using SeoulAir.Device.Api.ViewModels;
using SeoulAir.Device.Domain.Dtos;

namespace SeoulAir.Device.Api.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CurrentConfigurationModel, DeviceSettings>()
                .ReverseMap();
        }
    }
}
