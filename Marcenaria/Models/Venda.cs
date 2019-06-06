using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marcenaria.Models
{
    public class Venda
    {
        public long Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        public List<ContaReceber> ContasReceber { get; set; }
        public Cliente Cliente { get; set; }
        public Entrega Entrega { get; set; }
        public Funcionario Funcionario { get; set; }
        public List<ItemVenda> ItensVenda { get; set; }
        
    }
}