using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marcenaria.Models
{
    public class Compra
    {
        public long Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public List<ContaPagar> ContasPagar { get; set; }
        public Funcionario Funcionario { get; set; }
        public List<ItemCompra> ItensCompra { get; set; }
        
    }
}