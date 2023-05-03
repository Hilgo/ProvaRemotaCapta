using System.ComponentModel.DataAnnotations;

namespace ProvaRemotaCapta.Data.Models;

public partial class Cliente
{
    [Key]
    [Required]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo Cpf é obrigatório")]
    public string Cpf { get; set; } = null!;

    public int IdTipoCliente { get; set; }

    public string Sexo { get; set; } = null!;

    public int IdSituacao { get; set; }

    public virtual Situacao Situacao { get; set; } = null!;

    public virtual TipoCliente TipoCliente { get; set; } = null!;
}
