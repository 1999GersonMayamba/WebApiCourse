using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Section8_ExceptionFilter.Model
{
    public class Tb_Cliente
    {
        [Required]
        public string Nome { get; set; }
        [Range(0,20)]
        public int Idade { get; set; }
    }
}