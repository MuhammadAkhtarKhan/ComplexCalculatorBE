using ComplexCalculator.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculator.Application.Contracts.Calculator
{
    public interface ICalculator
    {
        public Task<CalculatorResponseModel> GetAllByUserId(string UserId);
        public Task<string> AddCalculation(CalculatorResponseModel calculatorModel);
        public Task<string> AddMultiple(List<CalculatorResponseModel> lstCalculations);
        public Task<CalculatorResponseModel> GetLatest(string UserId);
        public Task<int> GetLatestBatchNo(string UserId);
        public Task<List<CalculatorSumModel>> GetAllSum(string UserId, int VersionValue, int BatchNo);
       
    }
}
