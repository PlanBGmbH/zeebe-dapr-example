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
        /// the Zeebe workflow engine.
        /// </summary>
        [HttpPost("/calc")]
        public CalcResponse Calc([FromBody] CalcRequest request)
        {
            return new()
            {
                Result = request.Operator switch
                {
                    "+" => request.FirstOperand.GetValueOrDefault() + request.SecondOperand.GetValueOrDefault(),
                    "-" => request.FirstOperand.GetValueOrDefault() - request.SecondOperand.GetValueOrDefault(),
                    "/" => request.FirstOperand.GetValueOrDefault() / request.SecondOperand.GetValueOrDefault(),
                    "*" => request.FirstOperand.GetValueOrDefault() * request.SecondOperand.GetValueOrDefault(),
                    _ => throw new InvalidOperationException($"Operator {request.Operator} isn't supported")
                }
            };
        }
    }
}
