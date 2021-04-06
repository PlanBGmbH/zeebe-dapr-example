using System;
using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models
{
    public class CalcRequest
    {
        [Required]
        public string Operator { get; set; }

        [Required]
        public Double? FirstOperand { get; set; }

        [Required]
        public Double? SecondOperand { get; set; }
    }
}
