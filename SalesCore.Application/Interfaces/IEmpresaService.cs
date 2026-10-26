using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesCore.Application.DTOs;

namespace SalesCore.Application.Interfaces
{
    public interface IEmpresaService
    {
        Task<List<EmpresaResponseDto>> GetAllAsync();
        Task<EmpresaResponseDto?> GetByIdAsync(int id);
        Task<EmpresaResponseDto> CreateAsync(EmpresaCreateDto dto);
        Task<EmpresaResponseDto?> UpdateAsync(int id, EmpresaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<ContatoResponseDto>?> GetContatosAsync(int idEmpresa);
    }
}
