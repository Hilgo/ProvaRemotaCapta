using AutoMapper;
using ProvaRemotaCapta.Data.Dtos;
using ProvaRemotaCapta.Data.Models;

namespace ProvaRemotaCapta.Profiles
{
    public class TipoClienteProfile : Profile
    {
        public TipoClienteProfile() {
            CreateMap<TipoClienteDto, TipoCliente>();
            CreateMap<TipoCliente, TipoClienteDto>();
        }
    }
}
