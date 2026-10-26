using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesCore.Application.DTOs
{
    public class ContatoUpdateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Cargo { get; set; }
        public bool Ativo { get; set; }
        public int IdEmpresa { get; set; }
    }
}
