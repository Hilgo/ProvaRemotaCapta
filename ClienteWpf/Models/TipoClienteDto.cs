using System.ComponentModel.DataAnnotations;

namespace ClienteWpf.Models
{
    public class TipoClienteDto
    {
        [Required]
        public int IdTipoCliente { get; set; }
        [Required]
        [StringLength(250)]
        public string Nome { get; set; }
    }
}