using AutoMapper;
using SeoulAir.Device.Api.ViewModels;
using SeoulAir.Device.Domain.Dtos;

namespace SeoulAir.Device.Api.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullDestinationValues = true;
            CreateMap<CurrentConfigurationModel, DeviceSettings>()
                .ForMember(model => model.Type, settings => settings.MapFrom(dto => dto.DeviceType))
                .ReverseMap();
        }
    }
}
