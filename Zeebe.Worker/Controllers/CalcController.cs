using System;
using Microsoft.AspNetCore.Mvc;
using Zeebe.Worker.Models;

namespace Zeebe.Worker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
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
