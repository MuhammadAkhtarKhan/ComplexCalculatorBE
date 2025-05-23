﻿using ComplexCalculator.Infrastructure.Identity;
using ComplexCalculator.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ComplexCalculator.Application.Contracts.Calculator;
using ComplexCalculator.Application.Models;
using System.Diagnostics;

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
            //var stopwatch = Stopwatch.StartNew();

            var result = await this._calculator.AddMultiple(calculatorModel);

            //stopwatch.Stop();

            //var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            return Ok(result);
        }

        //write a get method to get all data
        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var result = await this._calculator.GetAll();
            return Ok(result);
        }
        //write a get method to get all data by endThread   
        [HttpGet(nameof(GetAllEndThreadByGroupNo))]
        public async Task<IActionResult> GetAllEndThreadByGroupNo(int groupNo)
        {
            var result = await this._calculator.GetAllEndThreadByGroupNo(groupNo);
            return Ok(result);
        }

        [HttpGet(nameof(GetAllByGroupNo))]
        public async Task<IActionResult> GetAllByGroupNo(int groupNo)
        {
            var result = await this._calculator.GetAllByGroupNo(groupNo);
            return Ok(result);
        }

        //write a delete method to delete all data by userId
        [HttpDelete(nameof(DeleteAllByUserId))]
        public async Task<IActionResult> DeleteAllByUserId(string userId)
        {
            var result = await this._calculator.DeleteAllByUserId(userId);
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

        [HttpPut("UpdateShutting/{UserId}/{VersionValue}/{BatchNo}")]
        public async Task<IActionResult> UpdateShutting(string UserId, int VersionValue, int BatchNo, int OpenValue)
        {
            var result = await this._calculator.UpdateShutting(UserId, VersionValue, BatchNo, OpenValue);
            return Ok(result);
        }

        [HttpPut("UpdateGroupNo/{UserId}/{VersionValue}/{BatchNo}")]
        public async Task<IActionResult> UpdateGroupNo(string UserId, int VersionValue, int BatchNo, int GroupNo)
        {
            var result = await this._calculator.UpdateGroupNo(UserId, VersionValue, BatchNo, GroupNo);
            return Ok(result);
        }

    }
}
