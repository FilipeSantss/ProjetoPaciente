using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjeto.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required]
        public string Doenca { get; set; }

        public string CaminhoArquivo { get; set; }

        public DateTime? DataDiagnostico { get; set; } = DateTime.Now;
        public string CaminhoPdf { get; set; }

    }
}
