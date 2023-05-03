using System.ComponentModel.DataAnnotations;

namespace ProvaRemotaCapta.Data.Dtos
{
    public class TipoClienteDto
    {

        [Required] public int IdTipoCliente { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "O campo Nome do Tipo Cliente deve até 250 caracteres.")]
        public string Nome { get; set;}
    }
}
