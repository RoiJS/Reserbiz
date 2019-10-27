using AutoMapper;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Helpers
{
    public class ReserbizAutoMapperProfile: Profile
    {
        public ReserbizAutoMapperProfile()
        {
            CreateMap<Client, ClientForRegisterDto>();
        }
    }
}