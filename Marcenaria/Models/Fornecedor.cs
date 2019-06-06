using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcenaria.Models
{
    public class Fornecedor
    {
        public long Id { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
    }
}