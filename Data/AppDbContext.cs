using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProjeto.Models;

namespace WebProjeto.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<MovimentacaoEstoque> MovimentacaoEstoque { get; set; }
    }
}
