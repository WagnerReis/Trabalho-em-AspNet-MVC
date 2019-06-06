using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcenaria.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Boolean Tipo { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public Usuario Usuario { get; set; }


    }
}