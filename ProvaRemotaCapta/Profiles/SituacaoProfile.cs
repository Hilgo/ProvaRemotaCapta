using AutoMapper;
using ProvaRemotaCapta.Data.Dtos;
using ProvaRemotaCapta.Data.Models;

namespace ProvaRemotaCapta.Profiles
{
    public class SituacaoProfile : Profile
    {
        public SituacaoProfile() {
            CreateMap<SituacaoDto, Situacao>();
            CreateMap<Situacao, SituacaoDto>();
        }
    }
}
