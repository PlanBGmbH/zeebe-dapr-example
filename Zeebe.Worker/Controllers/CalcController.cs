using System;

using Microsoft.AspNetCore.Mvc;

using Zeebe.Worker.Models;

namespace Zeebe.Worker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        /// <summary>
        /// This action gets called from the Zeebe Dapr binding for each calc job that is executed inside
        /// the Zeebe process engine.
        /// </summary>
        [HttpPost("/calc")]
        public CalcResponse Calc([FromBody] CalcRequest request)
        {
            var (@operator, firstOperand, secondOperand) = request;
            var first = firstOperand.GetValueOrDefault();
            var second = secondOperand.GetValueOrDefault();
            return new CalcResponse(@operator switch
            {
                "+" => first + second,
                "-" => first - second,
                "/" => first / second,
                "*" => first * second,
                _ => throw new InvalidOperationException($"Operator {@operator} isn't supported")
            });
        }
    }
}