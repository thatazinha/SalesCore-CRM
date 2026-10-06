using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCore.Domain.Entities;
using SalesCore.Infrastructure.Data;

namespace SalesCore.API.Controllers;

[ApiController]
[Route("contatos")]
public class ContatosController : ControllerBase
{
    private readonly SalesCoreDbContext _context;

    public ContatosController(SalesCoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var contatos = await _context.Contatos.ToListAsync();

        return Ok(contatos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contato = await _context.Contatos
            .Include(c => c.Empresa)
            .FirstOrDefaultAsync(c => c.IdContato == id);

        if (contato == null)
        {
            return NotFound();
        }

        //return Ok(contato);
        return Ok(new
        {
            contato.IdContato,
            contato.Nome,
            contato.Email,
            contato.Telefone,
            contato.Cargo,
            contato.Ativo,
            Empresa = new
            {
                contato.Empresa.IdEmpresa,
                contato.Empresa.RazaoSocial,
                contato.Empresa.NomeFantasia
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Post(Contato contato)
    {
        _context.Contatos.Add(contato);

        await _context.SaveChangesAsync();

        return Ok(contato);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Contato contato)
    {
        var contatoExistente = await _context.Contatos.FindAsync(id);

        if (contatoExistente == null)
        {
            return NotFound();
        }

        contatoExistente.Nome = contato.Nome;
        contatoExistente.Email = contato.Email;
        contatoExistente.Telefone = contato.Telefone;
        contatoExistente.Cargo = contato.Cargo;
        contatoExistente.Ativo = contato.Ativo;
        contatoExistente.IdEmpresa = contato.IdEmpresa;

        await _context.SaveChangesAsync();

        return Ok(contatoExistente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);

        if (contato == null)
        {
            return NotFound();
        }

        _context.Contatos.Remove(contato);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
