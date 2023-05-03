using AutoMapper;
using ProvaRemotaCapta.Data.Dtos;
using ProvaRemotaCapta.Data.Models;

namespace ProvaRemotaCapta.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteDto, Cliente>();
            CreateMap<Cliente, ClienteDto>()
                .ForMember(clienteDto => clienteDto.TipoCliente, opt => opt.MapFrom(cliente => cliente.TipoCliente))
                .ForMember(clienteDto =>clienteDto.Situacao, opt => opt.MapFrom(cliente =>cliente.Situacao))
                ;
            CreateMap<UpdateClienteDto, Cliente>();
        }

    }
}
