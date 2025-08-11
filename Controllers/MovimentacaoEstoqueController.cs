using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProjeto.Data;
using WebProjeto.Models;

public class MovimentacaoEstoqueController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public MovimentacaoEstoqueController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public IActionResult AtualizarEstoque(int id, int quantidade, string tipo)
    {
        var produto = _context.Produtos.Find(id);
        if (produto != null)
        {
            if (tipo == "Entrada")
                produto.Quantidade += quantidade;
            else if (tipo == "Saída")
                produto.Quantidade -= quantidade;

            var userId = _userManager.GetUserId(User);

            _context.MovimentacaoEstoque.Add(new MovimentacaoEstoque
            {
                ProdutoId = id,
                Quantidade = quantidade,
                DataMovimentacao = DateTime.Now,
                Tipo = tipo,
                UserId = userId  // associa ao usuário logado
            });

            _context.SaveChanges();
        }

        return RedirectToAction("Estoque", "Produtos");
    }
}
