using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebProjeto.Data;
using WebProjeto.Models;

[Authorize]
public class ProdutosController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ProdutosController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);
        var produtos = _context.Produtos.Where(p => p.UserId == userId).ToList();
        return View(produtos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Produtos produtos)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            ModelState.AddModelError("", "Usuário não autenticado.");
            return View(produtos);
        }

        produtos.UserId = userId;
        produtos.Quantidade = 0;

        _context.Produtos.Add(produtos);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id && p.UserId == userId);

        if (produto == null) return NotFound();

        return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Produtos produtos)
    {
        if (ModelState.IsValid)
        {
            var userId = _userManager.GetUserId(User);
            if (produtos.UserId != userId)
                return Unauthorized();

            produtos.UserId = userId;
            _context.Produtos.Update(produtos);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(produtos);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id && p.UserId == userId);

        if (produto == null) return NotFound();

        return View(produto);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var userId = _userManager.GetUserId(User);
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id && p.UserId == userId);

        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id && p.UserId == userId);

        if (produto == null) return NotFound();

        return View(produto);
    }

    public IActionResult Estoque()
    {
        var userId = _userManager.GetUserId(User);
        var produtos = _context.Produtos.Where(p => p.UserId == userId).ToList();
        return View(produtos);
    }

    [HttpPost]
    public IActionResult AtualizarEstoque(int id, int quantidade)
    {
        var userId = _userManager.GetUserId(User);
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id && p.UserId == userId);

        if (produto != null)
        {
            produto.Quantidade += quantidade;
            _context.SaveChanges();
        }
        return RedirectToAction("Estoque");
    }

    public IActionResult Buscar(string termo)
    {
        var userId = _userManager.GetUserId(User);
        var resultados = _context.Produtos
            .Where(p => p.UserId == userId &&
                        (p.Codigo.Contains(termo) || p.Descricao.Contains(termo)))
            .ToList();

        ViewBag.Termo = termo;
        return View("Index", resultados);
    }
}
