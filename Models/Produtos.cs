using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjeto.Models
{
    public class Produtos
    {

        
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string Codigo { get; set; }
        [Required]
        public int Quantidade { get; set; } = 0;
        [Required]

        public string UserId { get; set; }


    }
}
