
using Microsoft.EntityFrameworkCore;
using SalesCore.Application.DTOs;
using SalesCore.Application.Interfaces;
using SalesCore.Domain.Entities;
using SalesCore.Infrastructure.Data;

namespace SalesCore.Infrastructure.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly SalesCoreDbContext _context;

        public EmpresaService(SalesCoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpresaResponseDto>> GetAllAsync()
        {
            return await _context.Empresas
                .AsNoTracking()
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
        }

        public async Task<EmpresaResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Empresas
                .AsNoTracking()
                .Where(empresa => empresa.IdEmpresa == id)
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
                .FirstOrDefaultAsync();
        }

        public async Task<EmpresaResponseDto> CreateAsync(EmpresaCreateDto dto)
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

            return new EmpresaResponseDto
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
        }

        public async Task<EmpresaResponseDto?> UpdateAsync(
            int id,
            EmpresaUpdateDto dto)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return null;
            }

            empresa.RazaoSocial = dto.RazaoSocial;
            empresa.NomeFantasia = dto.NomeFantasia;
            empresa.Cnpj = dto.Cnpj;
            empresa.Email = dto.Email;
            empresa.Telefone = dto.Telefone;
            empresa.Ativo = dto.Ativo;

            await _context.SaveChangesAsync();

            return new EmpresaResponseDto
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
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return false;
            }

            _context.Empresas.Remove(empresa);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ContatoResponseDto>?> GetContatosAsync(int idEmpresa)
        {
            var empresaExiste = await _context.Empresas
                .AsNoTracking()
                .AnyAsync(e => e.IdEmpresa == idEmpresa);

            if (!empresaExiste)
            {
                return null;
            }

            return await _context.Contatos
                .AsNoTracking()
                .Where(c => c.IdEmpresa == idEmpresa)
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
        }



    }
}
