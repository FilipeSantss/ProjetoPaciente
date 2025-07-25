using WebProjeto.Models;
using WebProjeto.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class DiagnosticoController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public DiagnosticoController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // Listar diagnósticos do paciente
    public IActionResult Index(int clienteId)
    {
        var diagnosticos = _context.Diagnosticos.Where(d => d.ClienteId == clienteId).ToList();
        ViewBag.ClienteId = clienteId;
        return View(diagnosticos);
    }

    // Form para criar diagnóstico
    public IActionResult Create(int clienteId)
    {
        ViewBag.ClienteId = clienteId;
        var model = new Diagnostico { ClienteId = clienteId, DataDiagnostico = DateTime.Now };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Diagnostico diagnostico)
    {
        if (ModelState.IsValid)
        {
            var pasta = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(pasta)) Directory.CreateDirectory(pasta);

            var nomeArquivo = $"{Guid.NewGuid()}_diagnostico.pdf";
            var caminho = Path.Combine(pasta, nomeArquivo);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Header().Text("Laudo de Diagnóstico").FontSize(20).SemiBold().AlignCenter();
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Paciente ID: {diagnostico.ClienteId}");
                        col.Item().Text($"Doença: {diagnostico.Doenca}");
                        col.Item().Text($"Data: {diagnostico.DataDiagnostico:dd/MM/yyyy}");
                    });
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Gerado em: ");
                        x.Span(DateTime.Now.ToString("dd/MM/yyyy"));
                    });
                });
            }).GeneratePdf(caminho);

            // Debug
            if (System.IO.File.Exists(caminho))
            {
                Console.WriteLine($"PDF criado em: {caminho}");
            }
            else
            {
                Console.WriteLine("Erro: PDF não foi criado.");
            }

            diagnostico.CaminhoPdf = $"/uploads/{nomeArquivo}";
            _context.Diagnosticos.Add(diagnostico);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { clienteId = diagnostico.ClienteId });
        }

        ViewBag.ClienteId = diagnostico.ClienteId;
        return View(diagnostico);
    }


    // Download do PDF
    public IActionResult DownloadPdf(int id)
    {
        var diagnostico = _context.Diagnosticos.Find(id);
        if (diagnostico == null || string.IsNullOrEmpty(diagnostico.CaminhoPdf))
            return NotFound();

        var caminhoAbsoluto = Path.Combine(_env.WebRootPath, diagnostico.CaminhoPdf.TrimStart('/'));
        var tipoMime = "application/pdf";
        return PhysicalFile(caminhoAbsoluto, tipoMime, Path.GetFileName(caminhoAbsoluto));


    }


}

