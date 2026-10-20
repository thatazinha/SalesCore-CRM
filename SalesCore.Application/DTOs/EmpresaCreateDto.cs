using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesCore.Application.DTOs
{
    public class EmpresaCreateDto
    {
        public string RazaoSocial { get; set; } = string.Empty;
        public string? NomeFantasia { get; set; }
        public string? Cnpj { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}
