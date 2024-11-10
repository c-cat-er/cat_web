using AutoMapper;
using modpackApi.DTO;

namespace modpackApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<modpackApi.Models.StoreLocation, StoreLocationDTO>();
            CreateMap<StoreLocationDTO, modpackApi.Models.StoreLocation>();
        }
    }
}
