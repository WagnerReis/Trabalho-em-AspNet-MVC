using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcenaria.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}