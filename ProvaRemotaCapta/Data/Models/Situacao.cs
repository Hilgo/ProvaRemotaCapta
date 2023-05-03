using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaRemotaCapta.Data.Models;

public partial class Situacao
{
    [Key]
    [Required]
    public int IdSituacao { get; set; }

    [Required(ErrorMessage = "O campo nome da situação é obrigatório")]
    public string NomeSituacao { get; set; } = null!;

    public string? SiglaSituacao { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
