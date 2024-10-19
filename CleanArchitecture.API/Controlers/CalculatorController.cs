using ComplexCalculator.Infrastructure.Identity;
using ComplexCalculator.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ComplexCalculator.Application.Contracts.Calculator;
using ComplexCalculator.Application.Models;

namespace ComplexCalculator.API.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator _calculator;
        public CalculatorController(ICalculator calculator)
        {
            this._calculator = calculator;
        }


        [HttpGet(nameof(GetLatest))]
        public async Task<IActionResult> GetLatest(string userId)
        {
            var result = await this._calculator.GetLatest(userId);
            return Ok(new { result = result });
        }

        [HttpPost(nameof(AddData))]
        public async Task<IActionResult> AddData([FromBody] CalculatorModel calculatorModel)
        {
            var result = await this._calculator.AddCalculation(calculatorModel);
            return Ok(result);
        }

        [HttpPost(nameof(AddMultiple))]
        public async Task<IActionResult> AddMultiple([FromBody] List<CalculatorModel> calculatorModel)
        {
            var result = await this._calculator.AddMultiple(calculatorModel);

            return Ok(result);
        }

        [HttpGet(nameof(GetAllSum))]
        public async Task<IActionResult> GetAllSum(string userId)
        {
            var result = await this._calculator.GetAllSum(userId);
            return Ok(result);
        }

    }
}
