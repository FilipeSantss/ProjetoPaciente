using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebProjeto.Data;
using WebProjeto.Models;
using System.Linq;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);

        var viewModel = new Dashboard
        {
            TotalClientes = _context.Clientes.Count(c => c.UserId == userId),
            TotalProdutos = _context.Produtos.Count(p => p.UserId == userId),
            Produtos = _context.Produtos
                .Where(p => p.UserId == userId)
                .ToList()
        };

        return View(viewModel);
    }
}
