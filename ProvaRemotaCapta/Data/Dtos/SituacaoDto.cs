using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ProvaRemotaCapta.Data.Dtos
{
    public class SituacaoDto
    {
        [Required]
        public int IdSituacao { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "O campo Nome da Situação deve até 250 caracteres.")]
        public string NomeSituacao { get; set; }
        [Required]
        [StringLength(2, ErrorMessage = "O campo Sigla da Situação deve até 2 caracteres.")]
        public string SiglaSituacao { get; set; }
    }
}
