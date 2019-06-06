using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marcenaria.Models
{
    public class ItemCompra
    {
        public long Id { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public Produto Produto { get; set; }
        public Compra Compra { get; set; }

    }
}