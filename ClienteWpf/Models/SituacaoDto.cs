using System.ComponentModel.DataAnnotations;

namespace ClienteWpf.Models
{
    public class SituacaoDto
    {
        [Required]
        public int IdSituacao { get; set; }
        [Required]
        [StringLength(250)]
        public string NomeSituacao { get; set; }
        [Required]
        [StringLength(2)]
        public string SiglaSituacao { get; set; }
    }
}