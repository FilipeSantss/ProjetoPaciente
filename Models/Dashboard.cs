// Em WebProjeto/ViewModels/DashboardViewModel.cs
namespace WebProjeto.Models
{
    public class Dashboard
    {
        public int TotalClientes { get; set; }
        public int TotalProdutos { get; set; }
        public List<WebProjeto.Models.Produtos> Produtos { get; set; } = new List<Produtos>();
    }
}
