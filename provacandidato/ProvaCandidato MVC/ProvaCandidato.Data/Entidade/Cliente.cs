using ProvaCandidato.Data.CustomValidations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [StringLength(50)]
        [MinLength(3, ErrorMessage = "o campo nome precisa ter no mínimo  3 caracteres")]
        [MaxLength(50, ErrorMessage = "o campo nome precisa ter no máximo 50 caracteres")]
        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Column("data_nascimento")]
        [DatetimeCompare]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? DataNascimento { get; set; }

        [Column("codigo_cidade")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("CidadeId")]
        public virtual Cidade Cidade { get; set; }

    }
}