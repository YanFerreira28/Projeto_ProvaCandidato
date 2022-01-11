using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cidade")]
    public class Cidade
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("nome")]
        [StringLength(50)]
        [MinLength(3, ErrorMessage = "o campo nome precisa ter no mínimo  3 caracteres")]
        [MaxLength(50, ErrorMessage = "o campo nome precisa ter no máximo 50 caracteres")]
        [Required]
        public string Nome { get; set; }
    }
}
