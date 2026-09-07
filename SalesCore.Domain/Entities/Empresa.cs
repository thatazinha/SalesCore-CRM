using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesCore.Domain.Entities
{
    public  class Empresa
    {
        public int IdEmpresa { get; set; }

        public string RazaoSocial { get; set; } = string.Empty;

        public string? NomeFantasia { get; set; }

        public string? Cnpj { get; set; }

        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }
    }
}
