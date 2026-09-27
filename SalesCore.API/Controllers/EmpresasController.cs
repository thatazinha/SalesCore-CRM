using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCore.Domain.Entities;
using SalesCore.Infrastructure.Data;

namespace SalesCore.API.Controllers;

[ApiController]
[Route("empresas")]
public class EmpresasController : ControllerBase
{
    private readonly SalesCoreDbContext _context;
    public EmpresasController(SalesCoreDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var empresas = await _context.Empresas.ToListAsync();

        return Ok(empresas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);

        if (empresa == null)
        {
            return NotFound();
        }

        return Ok(empresa);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Empresa empresa)
    {
        _context.Empresas.Add(empresa);

        await _context.SaveChangesAsync();

        return Ok(empresa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Empresa empresa)
    {
        var empresaExistente = await _context.Empresas.FindAsync(id);

        if (empresaExistente == null)
        {
            return NotFound();
        }

        empresaExistente.RazaoSocial = empresa.RazaoSocial;
        empresaExistente.NomeFantasia = empresa.NomeFantasia;
        empresaExistente.Cnpj = empresa.Cnpj;
        empresaExistente.Email = empresa.Email;
        empresaExistente.Telefone = empresa.Telefone;
        empresaExistente.DataCadastro = empresa.DataCadastro;
        empresaExistente.Ativo = empresa.Ativo;

        await _context.SaveChangesAsync();

        return Ok(empresaExistente);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);

        if (empresa == null)
        {
            return NotFound();
        }

        _context.Empresas.Remove(empresa);

        await _context.SaveChangesAsync();

        return Ok();
    }

}
