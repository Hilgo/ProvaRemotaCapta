using AutoMapper;
using ProvaRemotaCapta.Data;
using ProvaRemotaCapta.Data.Dtos;
using ProvaRemotaCapta.Data.Models;

namespace ProvaRemotaCapta.Services
{
    public class SituacaoService
    {
        private IMapper _mapper;
        private ProvaCaptaContext _context;

        public SituacaoService(IMapper mapper, ProvaCaptaContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public List<SituacaoDto> RecuperaSituacoes(string? nomeSituacao)
        {
            var situacoes = _context.Situacoes.ToList();
            if (situacoes == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nomeSituacao))
            {
                IEnumerable<Situacao> query = _context.Situacoes.Where(s => s.NomeSituacao == nomeSituacao);

                situacoes = query.ToList();
            }
            return _mapper.Map<List<SituacaoDto>>(situacoes);
        }
        public SituacaoDto RecuperaSituacaoPorId(int id)
        {
            var situacao = new Situacao();
            IEnumerable<Situacao> query = _context.Situacoes.Where(s => s.IdSituacao == id);
            situacao = query.FirstOrDefault();
            return _mapper.Map<SituacaoDto>(situacao);
        }
    }
}
