using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCore.Domain.Entities;
using SalesCore.Infrastructure.Data;
using SalesCore.Application.DTOs;

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
        var contatos = await _context.Contatos
            .Select(c => new ContatoResponseDto
            {
                IdContato = c.IdContato,
                Nome = c.Nome,
                Email = c.Email,
                Telefone = c.Telefone,
                Cargo = c.Cargo,
                Ativo = c.Ativo,
                IdEmpresa = c.IdEmpresa
            })
            .ToListAsync();

        return Ok(contatos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contato = await _context.Contatos
            .Where(c => c.IdContato == id)
            .Select(c => new ContatoResponseDto
            {
                IdContato = c.IdContato,
                Nome = c.Nome,
                Email = c.Email,
                Telefone = c.Telefone,
                Cargo = c.Cargo,
                Ativo = c.Ativo,
                IdEmpresa = c.IdEmpresa
            })
            .FirstOrDefaultAsync();

        if (contato == null)
            return NotFound();

        return Ok(contato);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ContatoCreateDto dto)
    {
        var empresa = await _context.Empresas
            .FindAsync(dto.IdEmpresa);

        if (empresa == null)
            return NotFound("Empresa não encontrada.");

        var contato = new Contato
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            Cargo = dto.Cargo,
            IdEmpresa = dto.IdEmpresa,
            Ativo = true
        };

        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();

        var resposta = new ContatoResponseDto
        {
            IdContato = contato.IdContato,
            Nome = contato.Nome,
            Email = contato.Email,
            Telefone = contato.Telefone,
            Cargo = contato.Cargo,
            Ativo = contato.Ativo,
            IdEmpresa = contato.IdEmpresa
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = contato.IdContato },
            resposta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(
        int id,
        ContatoUpdateDto dto)
    {
        var contato = await _context.Contatos.FindAsync(id);

        if (contato == null)
            return NotFound();

        var empresa = await _context.Empresas
            .FindAsync(dto.IdEmpresa);

        if (empresa == null)
            return NotFound("Empresa não encontrada.");

        contato.Nome = dto.Nome;
        contato.Email = dto.Email;
        contato.Telefone = dto.Telefone;
        contato.Cargo = dto.Cargo;
        contato.Ativo = dto.Ativo;
        contato.IdEmpresa = dto.IdEmpresa;

        await _context.SaveChangesAsync();

        var resposta = new ContatoResponseDto
        {
            IdContato = contato.IdContato,
            Nome = contato.Nome,
            Email = contato.Email,
            Telefone = contato.Telefone,
            Cargo = contato.Cargo,
            Ativo = contato.Ativo,
            IdEmpresa = contato.IdEmpresa
        };

        return Ok(resposta);
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

        return NoContent();
    }
}
