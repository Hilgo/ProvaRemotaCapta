using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProvaRemotaCapta.Data;
using ProvaRemotaCapta.Data.Dtos;
using ProvaRemotaCapta.Data.Models;

namespace ProvaRemotaCapta.Services
{
    public class TipoService
    {
        private IMapper _mapper;
        private ProvaCaptaContext _context;

        public TipoService(IMapper mapper, ProvaCaptaContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<TipoClienteDto> RecuperaTipos(string? nomeTipo)
        {
            var tipos = _context.TipoClientes.ToList();
            if (tipos == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nomeTipo))
            {
                IEnumerable<TipoCliente> query = _context.TipoClientes.Where(c => c.Nome == nomeTipo);

                tipos = query.ToList();
            }
            return _mapper.Map<List<TipoClienteDto>>(tipos);
        }

        public TipoClienteDto RecuperaTipoPorId(int id)
        {
            var tipo = new TipoCliente();
            IEnumerable<TipoCliente> query = _context.TipoClientes.Where(c => c.IdTipoCliente == id);
            tipo = query.FirstOrDefault();
            return _mapper.Map<TipoClienteDto>(tipo);
        }
    }
}
