using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCore.Domain.Entities;
using SalesCore.Infrastructure.Data;
using SalesCore.Application.DTOs;

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
        var empresas = await _context.Empresas
            .Select(empresa => new EmpresaResponseDto
            {
                IdEmpresa = empresa.IdEmpresa,
                RazaoSocial = empresa.RazaoSocial,
                NomeFantasia = empresa.NomeFantasia,
                Cnpj = empresa.Cnpj,
                Email = empresa.Email,
                Telefone = empresa.Telefone,
                DataCadastro = empresa.DataCadastro,
                Ativo = empresa.Ativo
            })
            .ToListAsync();

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

        var dto = new EmpresaResponseDto
        {
            IdEmpresa = empresa.IdEmpresa,
            RazaoSocial = empresa.RazaoSocial,
            NomeFantasia = empresa.NomeFantasia,
            Cnpj = empresa.Cnpj,
            Email = empresa.Email,
            Telefone = empresa.Telefone,
            DataCadastro = empresa.DataCadastro,
            Ativo = empresa.Ativo
        };

        return Ok(dto);
    }


    [HttpGet("{id}/contatos")]
    public async Task<IActionResult> GetContatos(int id)
    {
        var empresa = await _context.Empresas.FindAsync(id);

        if (empresa == null)
        {
            return NotFound();
        }

        var contatos = await _context.Contatos
            .Where(c => c.IdEmpresa == id)
            .ToListAsync();

        return Ok(contatos);
    }



    [HttpPost]
    public async Task<IActionResult> Post(EmpresaCreateDto dto)
    {
        var empresa = new Empresa
        {
            RazaoSocial = dto.RazaoSocial,
            NomeFantasia = dto.NomeFantasia,
            Cnpj = dto.Cnpj,
            Email = dto.Email,
            Telefone = dto.Telefone,
            DataCadastro = DateTime.Now,
            Ativo = true
        };

        _context.Empresas.Add(empresa);

        await _context.SaveChangesAsync();

        var resposta = new EmpresaResponseDto
        {
            IdEmpresa = empresa.IdEmpresa,
            RazaoSocial = empresa.RazaoSocial,
            NomeFantasia = empresa.NomeFantasia,
            Cnpj = empresa.Cnpj,
            Email = empresa.Email,
            Telefone = empresa.Telefone,
            DataCadastro = empresa.DataCadastro,
            Ativo = empresa.Ativo
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = empresa.IdEmpresa },
            resposta);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EmpresaUpdateDto dto)
    {
        var empresaExistente = await _context.Empresas.FindAsync(id);

        if (empresaExistente == null)
        {
            return NotFound();
        }

        empresaExistente.RazaoSocial = dto.RazaoSocial;
        empresaExistente.NomeFantasia = dto.NomeFantasia;
        empresaExistente.Cnpj = dto.Cnpj;
        empresaExistente.Email = dto.Email;
        empresaExistente.Telefone = dto.Telefone;
        empresaExistente.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();

        var resposta = new EmpresaResponseDto
        {
            IdEmpresa = empresaExistente.IdEmpresa,
            RazaoSocial = empresaExistente.RazaoSocial,
            NomeFantasia = empresaExistente.NomeFantasia,
            Cnpj = empresaExistente.Cnpj,
            Email = empresaExistente.Email,
            Telefone = empresaExistente.Telefone,
            DataCadastro = empresaExistente.DataCadastro,
            Ativo = empresaExistente.Ativo
        };

        return Ok(resposta);
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

        return NoContent();
    }

}
