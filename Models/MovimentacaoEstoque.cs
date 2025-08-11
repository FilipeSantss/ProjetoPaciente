namespace WebProjeto.Models
{
    public class MovimentacaoEstoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produtos Produto { get; set; }

        public int Quantidade { get; set; }  // positiva para entrada, negativa para saída
        public DateTime DataMovimentacao { get; set; }
        public string Tipo { get; set; } // "Entrada" ou "Saída"


        public string UserId { get; set; }
    }
}
