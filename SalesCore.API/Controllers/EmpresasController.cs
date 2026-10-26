using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCore.Application.DTOs;
using SalesCore.Application.Interfaces;
using SalesCore.Domain.Entities;
using SalesCore.Infrastructure.Data;

namespace SalesCore.API.Controllers;

[ApiController]
[Route("empresas")]
public class EmpresasController : ControllerBase
{
    private readonly SalesCoreDbContext _context;
    private readonly IEmpresaService _empresaService;

    public EmpresasController( SalesCoreDbContext context, IEmpresaService empresaService)
    {
        _context = context;
        _empresaService = empresaService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var empresas = await _empresaService.GetAllAsync();

        return Ok(empresas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var empresa = await _empresaService.GetByIdAsync(id);

        if (empresa == null)
        {
            return NotFound();
        }

        return Ok(empresa);
    }




    [HttpGet("{id}/contatos")]
    public async Task<IActionResult> GetContatos(int id)
    {
        var contatos = await _empresaService.GetContatosAsync(id);

        if (contatos == null)
        {
            return NotFound();
        }

        return Ok(contatos);
    }





    [HttpPost]
    public async Task<IActionResult> Post(EmpresaCreateDto dto)
    {
        var resposta = await _empresaService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = resposta.IdEmpresa },
            resposta);
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EmpresaUpdateDto dto)
    {
        var resposta = await _empresaService.UpdateAsync(id, dto);

        if (resposta == null)
        {
            return NotFound();
        }

        return Ok(resposta);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var excluida = await _empresaService.DeleteAsync(id);

        if (!excluida)
        {
            return NotFound();
        }

        return NoContent();
    }


}
