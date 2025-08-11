using System;
using System.ComponentModel.DataAnnotations;

namespace WebProjeto.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter até 100 caracteres")]
        public string Nome { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100)]
        public string? Email { get; set; }

        public DateTime? Nascimento { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF inválido")]
        public string CPF { get; set; } = null!;

        [StringLength(20)]
        public string? Telefone { get; set; }

        [StringLength(200)]
        public string? Endereco { get; set; }

        [StringLength(9)]
        public string? Cep { get; set; }

        [StringLength(100)]
        public string? Cidade { get; set; }

        public string? UserId { get; set; }
    }
}
