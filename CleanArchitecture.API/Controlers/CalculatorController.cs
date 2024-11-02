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

         [HttpGet(nameof(GetLatestBatchNo))]
        public async Task<IActionResult> GetLatestBatchNo(string UserId)
        {
            var result = await this._calculator.GetLatestBatchNo(UserId);
            return Ok(new { BatchNo = result });
        }

        [HttpPost(nameof(AddData))]
        public async Task<IActionResult> AddData([FromBody] CalculatorResponseModel calculatorModel)
        {
            var result = await this._calculator.AddCalculation(calculatorModel);
            return Ok(result);
        }

        [HttpPost(nameof(AddMultiple))]
        public async Task<IActionResult> AddMultiple([FromBody] List<CalculatorResponseModel> calculatorModel)
        {
            var result = await this._calculator.AddMultiple(calculatorModel);

            return Ok(result);
        }

        [HttpGet("GetAllSum/{UserId}/{VersionValue}/{BatchNo}")]
        public async Task<IActionResult> GetAllSum(string UserId, int VersionValue, int BatchNo)
        {
            var result = await this._calculator.GetAllSum(UserId, VersionValue, BatchNo);
            return Ok(result);
        }

        [HttpPut("UpdateTongshu/{UserId}/{VersionValue}/{BatchNo}")]
        public async Task<IActionResult> UpdateTongshu(string UserId, int VersionValue, int BatchNo, int Tongshu)
        {
            var result = await this._calculator.UpdateTongshu(UserId, VersionValue, BatchNo, Tongshu);
            return Ok(result);
        }

    }
}
