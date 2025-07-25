using WebProjeto.Data;
using Microsoft.AspNetCore.Mvc;
using WebProjeto.Models;

public class ClienteController : Controller
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    // Listar todos
    public IActionResult Index()
    {
        var clientes = _context.Clientes.ToList();
        return View(clientes);
    }

    // Exibir formulário de criação
    public IActionResult Create()
    {
        return View();
    }

    // Salvar novo cliente
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(cliente);
    }

    // Exibir formulário de edição
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();

        var cliente = _context.Clientes.Find(id);
        if (cliente == null) return NotFound();

        return View(cliente);
    }

    // Atualizar cliente
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    // Exibir confirmação de exclusão
    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();

        var cliente = _context.Clientes.Find(id);
        if (cliente == null) return NotFound();

        return View(cliente);
    }

    // Confirmar exclusão
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var cliente = _context.Clientes.Find(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Buscar(string termo)
    {
        var resultados = _context.Clientes
         .Where(c => c.Nome.Contains(termo) || c.CPF.Contains(termo))
         .ToList();

        ViewBag.Termo = termo;
        return View("Index", resultados);
    }

    public IActionResult Details(int? id)
    {
        if (id == null) return NotFound();

        var cliente = _context.Clientes.Find(id);
        if (cliente == null) return NotFound();

        return View(cliente);
    }


}
