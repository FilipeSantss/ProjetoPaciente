namespace WebProjeto.Models
{
    public class Cliente
    {

        // Relação: um cliente pode ter muitos diagnósticos
        public ICollection<Diagnostico> Diagnosticos { get; set; }
        public int Id { get; set; }
        public  string Nome { get; set; }

        public string Email { get; set; }

        public DateTime Nascimento { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
    }
}
