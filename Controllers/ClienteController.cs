using WebProjeto.Data;
using Microsoft.AspNetCore.Mvc;
using WebProjeto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;

[Authorize]
public class ClienteController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ClienteController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Listar clientes só do usuário logado
    public IActionResult Index()
    {
        var userId = _userManager.GetUserId(User);
        var clientes = _context.Clientes.Where(c => c.UserId == userId).ToList();
        return View(clientes);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cliente cliente)
    {
        if (cliente == null)
            return BadRequest("Cliente vazio");

        if (!ModelState.IsValid)
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Content("Erros: " + string.Join(", ", erros));
        }

        // Forçar UserId para teste
        cliente.UserId = _userManager.GetUserId(User);

        _context.Clientes.Add(cliente);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Cliente model)
    {
        var userId = _userManager.GetUserId(User);
        var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id && c.UserId == userId);
        if (cliente == null) return NotFound();

        if (!ModelState.IsValid) return View(model);

        cliente.Nome = model.Nome;
        cliente.CPF = model.CPF;
        cliente.Email = model.Email;
        cliente.Telefone = model.Telefone;
        cliente.Endereco = model.Endereco;
        cliente.Cep = model.Cep;
        cliente.Cidade = model.Cidade;
        cliente.Nascimento = model.Nascimento;

        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id && c.UserId == userId);
        if (cliente == null) return NotFound();

        return View(cliente);
    }


    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id && c.UserId == userId);
        if (cliente == null) return NotFound();

        return View(cliente);
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var userId = _userManager.GetUserId(User);
        var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id && c.UserId == userId);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Buscar(string termo)
    {
        var userId = _userManager.GetUserId(User);
        var resultados = _context.Clientes
            .Where(c => c.UserId == userId && (c.Nome.Contains(termo) || c.CPF.Contains(termo)))
            .ToList();

        ViewBag.Termo = termo;
        return View("Index", resultados);
    }

    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id && c.UserId == userId);
        if (cliente == null) return NotFound();

        return View(cliente);
    }
}
