using Marcenaria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marcenaria.Models
{
    public class ContaPagar
    {
        public long Id { get; set; }
        public decimal Valor { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        public Compra Compra { get; set; }
    }
}