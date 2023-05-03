using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaRemotaCapta.Data.Models;

public partial class TipoCliente
{
    [Key]
    [Required]
    public int IdTipoCliente { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string Nome { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
