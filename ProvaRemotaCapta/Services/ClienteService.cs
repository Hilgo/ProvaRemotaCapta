using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using ProvaRemotaCapta.Data;
using ProvaRemotaCapta.Data.Dtos;
using ProvaRemotaCapta.Data.Models;

namespace ProvaRemotaCapta.Services
{
    public class ClienteService
    {
        private IMapper _mapper;
        private ProvaCaptaContext _context;
        private TipoService _tipoService;
        private SituacaoService _situacaoService;

        public ClienteService(IMapper mapper, ProvaCaptaContext context,TipoService tipoService,SituacaoService situacaoService)
        {
            _mapper = mapper;
            _context = context;
            _tipoService = tipoService;
            _situacaoService = situacaoService;
            
        }

        public List<ClienteDto> RecuperaClientes(string? nomeCliente)
        {
            var clientes = _context.Clientes.ToList();
            if (clientes == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nomeCliente))
            {
                IEnumerable<Cliente> query = _context.Clientes.Where(c => c.Nome == nomeCliente);

                clientes = query.ToList();
            }
            return _mapper.Map<List<ClienteDto>>(clientes);
        }

        public ClienteDto RecuperaClientesPorId(int id)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.IdCliente == id);
            if (cliente != null)
            {
                return _mapper.Map<ClienteDto>(cliente);
            }
            return null;
        }
        public ClienteDto AdicionaCliente(ClienteDto clienteDto)
        {
            try
            {
                Cliente cliente = _mapper.Map<Cliente>(clienteDto);
                var clienteSalvo = RecuperaClientesPorId(cliente.IdCliente);
                if (clienteSalvo == null)
                {
                    cliente.IdCliente = 0;
                    _context.Attach(cliente.Situacao);
                    _context.Attach(cliente.TipoCliente);
                    _context.Clientes.Add(cliente);
                    _context.SaveChanges();
                }
                
                return _mapper.Map<ClienteDto>(cliente);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE"))
                {
                    //Violação de chave única
                }
                return null;
            }
            
        }

        public Result AtualizaCliente(int id, UpdateClienteDto updateClienteDto)
        {
            try
            {
                Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.IdCliente == id);
                if (cliente == null)
                {
                    return Result.Fail("Cliente não encontrado");
                }

                cliente.IdTipoCliente = updateClienteDto.IdTipoCliente;
                cliente.IdSituacao = updateClienteDto.IdSituacao;


                _mapper.Map(updateClienteDto, cliente);
                _context.SaveChanges();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                string mensagem = "Erro Ao Atualizar";
                if (ex.InnerException.Message.Contains("UNIQUE"))
                {
                    mensagem += " - Verifique CPF";
                }
                return Result.Fail(mensagem);
            }
            
        }

        public Result DeletaCliente(int id)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(cliente => cliente.IdCliente == id);
            if (cliente == null)
            {
                return Result.Fail("Cliente não encontrado");
            }
            _context.Remove(cliente);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
