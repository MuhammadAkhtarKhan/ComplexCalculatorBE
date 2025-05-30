﻿using ComplexCalculator.Infrastructure.Identity;
using ComplexCalculator.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ComplexCalculator.Application.Contracts.Calculator;
using ComplexCalculator.Application.Models;
using System.Diagnostics;
using ComplexCalculator.Domain.Entities;

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

    

        [HttpPost(nameof(AddMultiple))]
        public async Task<IActionResult> AddMultiple([FromBody] List<CalculatorResponseModel> calculatorModel)
        {
            //var stopwatch = Stopwatch.StartNew();

            var result = await this._calculator.AddMultiple(calculatorModel);

            //stopwatch.Stop();

            //var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

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

        // write a method to delete all data by userI and name  
        [HttpDelete(nameof(DeleteAllByNameAndUserId))]
        public async Task<IActionResult> DeleteAllByNameAndUserId(string userId, string name)
        {
            var result = await this._calculator.DeleteAllByNameAndUserId(userId,name);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteById))]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await this._calculator.DeleteById(id);
            return Ok(result);
        }
        // write method for AddTempCalculator   

        [HttpPost(nameof(AddOrUpdateTempCalculator))]
        public async Task<IActionResult> AddOrUpdateTempCalculator(TempCalculatorResponseModel calculator)
        {
           var result = await _calculator.AddOrUpdateTempCalculator(calculator);
            return Ok(result);
        }
           [HttpGet(nameof(GetAllTempCalculatorByGroupNo))]
        public async Task<IActionResult> GetAllTempCalculatorByGroupNo(int groupNo)
        {
           var result = await _calculator.GetAllTempCalculatorByGroupNo(groupNo);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteTempCalculator))]
        public async Task<IActionResult> DeleteTempCalculator(int id)
        {
           var res= await _calculator.DeleteTempCalculator(id);
            return Ok(res);
        }
         [HttpDelete(nameof(DeleteTempCalculatorByUserId))]
        public async Task<IActionResult> DeleteTempCalculatorByUserId(string userId)
        {
           var res= await _calculator.DeleteTempCalculatorByUserId(userId);
            return Ok(res);
        }

        [HttpGet("GetAllSum/{UserId}/{VersionValue}/{BatchNo}")]
        public async Task<IActionResult> GetAllSum(string UserId, int VersionValue, int BatchNo)
        {
            var result = await this._calculator.GetAllSum(UserId, VersionValue, BatchNo);
            return Ok(result);
        }  


    }
}
