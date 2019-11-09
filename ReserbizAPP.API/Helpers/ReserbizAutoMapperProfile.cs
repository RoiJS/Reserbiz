using AutoMapper;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Helpers
{
    public class ReserbizAutoMapperProfile : Profile
    {
        public ReserbizAutoMapperProfile()
        {
            CreateMap<Client, ClientDetailsDto>();
            CreateMap<Client, ClientForListDto>();
            CreateMap<Tenant, TenantDetailsDto>();
            CreateMap<Tenant, TenantForListDto>();
            CreateMap<Account, AccountForListDto>();
            CreateMap<Account, AccountForDetailDto>();

            CreateMap<TenantForCreationDto, Tenant>();
            CreateMap<TenantForUpdateDto, Tenant>();
            CreateMap<ClientForUpdateDto, Client>();

        }
    }
}