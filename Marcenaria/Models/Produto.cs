using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcenaria.Models
{
    public class Produto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        
    }
}