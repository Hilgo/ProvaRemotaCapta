using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ProvaRemotaCapta.Data.Dtos;
using ProvaRemotaCapta.Services;

namespace ProvaRemotaCapta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteCaptaController : ControllerBase
    {
        private ClienteService _clienteService;
        private TipoService _tipoService;
        private SituacaoService _situacaoService;
        public ClienteCaptaController(ClienteService clienteService,TipoService tipoService, SituacaoService situacaoService) {
            _clienteService = clienteService;
            _tipoService = tipoService;
            _situacaoService = situacaoService;
        }

        [HttpGet("GetClienteCapta/", Name = "GetClienteCapta")]

        public IActionResult RecuperaClientes(string? nomeCliente)
        {
            var readDto = _clienteService.RecuperaClientes(nomeCliente);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }
        [HttpGet("RecuperaClientesPorId/{idCliente}", Name = "RecuperaClientesPorId")]

        public IActionResult RecuperaClientesPorId(int idCliente)
        {
            var readDto = _clienteService.RecuperaClientesPorId(idCliente);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [HttpPost("AdicionaClienteCapta/", Name = "AdicionaClienteCapta")]

        public IActionResult AdicionaClienteCapta([FromBody] ClienteDto clienteDto)
        {
            ClienteDto readDto = _clienteService.AdicionaCliente(clienteDto);
            if (readDto == null) return BadRequest("Falha na inserção do cliente");
            return CreatedAtAction(nameof(RecuperaClientesPorId), new { idCliente = readDto.IdCliente  }, readDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCliente(int id, [FromBody] UpdateClienteDto clienteDto)
        {
            Result resultado = _clienteService.AtualizaCliente(id, clienteDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public IActionResult DeletaCliente(int id)
        {
            Result resultado = _clienteService.DeletaCliente(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpGet("GetTiposCapta/", Name = "GetTiposCapta")]
        public IActionResult RecuperaTipos(string? nomeTipo)
        {
            var readDto = _tipoService.RecuperaTipos(nomeTipo);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }

        [HttpGet("GetSituacoesCapta/", Name = "GetSituacoesCapta")]
        public IActionResult RecuperaSituacoes(string? nomeSituacao)
        {
            var readDto = _situacaoService.RecuperaSituacoes(nomeSituacao);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }
    }
}