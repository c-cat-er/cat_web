using AutoMapper;
using modpack.Models;
using modpack.ViewModels;
using modpackApi.DTO;

namespace modpack.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Administrator, CAdminViewModel>();
            CreateMap<StoreLocation, StoreLocationDTO>();
            CreateMap<StoreLocation, StoreLocationVM>();
            CreateMap<StoreLocationDTO, modpackApi.Models.StoreLocation>();
            CreateMap<StoreLocationDTO, StoreLocationVM>();
            CreateMap<StoreLocationVM, StoreLocationDTO>();
        }
    }
}
