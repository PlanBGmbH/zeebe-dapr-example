using System.ComponentModel.DataAnnotations;

namespace Zeebe.Worker.Models
{
    public record CalcRequest(
        [Required] string Operator,
        [Required] double? FirstOperand,
        [Required] double? SecondOperand);
}