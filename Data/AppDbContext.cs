using Microsoft.EntityFrameworkCore;
using WebProjeto.Models;

namespace WebProjeto.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Diagnostico> Diagnosticos { get; set; }
}
