using AutoMapper;
using DC = SensorApi.API.DataContracts;
using S = SensorApi.Services.Model;

namespace SensorApi.IoC.Configuration.AutoMapper.Profiles
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<DC.User, S.User>().ReverseMap();
            CreateMap<DC.Address, S.Address>().ReverseMap();
        }
    }
}
