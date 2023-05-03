using ClienteWpf.Data.Validator;
using System.ComponentModel.DataAnnotations;

namespace ClienteWpf.Models
{
    public class ClienteDto
    {
        [Required]
        public int IdCliente { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "O campo Nome deve até 250 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "O campo Cpf deve ter 11 dígitos.", MinimumLength = 11)]
        [ValidaCPF]
        public string Cpf { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "O campo Sexo deve até 200 caracteres.")]
        public string Sexo { get; set; }

        [Required]
        public TipoClienteDto tipoCliente { get; set; }
        [Required]
        public SituacaoDto Situacao { get; set; }

    }
}
