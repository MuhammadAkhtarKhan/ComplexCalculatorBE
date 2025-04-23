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
       
        public Task<List<CalculatorResponseModel>> GetAll();
        public Task<string> AddCalculation(CalculatorResponseModel calculatorModel);
        public Task<List<CalculatorResponseModel>> AddMultiple(List<CalculatorResponseModel> lstCalculations);
        public Task<string> DeleteAllByUserId(string UserId);
        public Task<CalculatorResponseModel> GetLatest(string UserId);
        public Task<int> GetLatestBatchNo(string UserId);
        public Task<CalculatorResponse> GetAllSum(string UserId, int VersionValue, int BatchNo);
        public Task<string> UpdateTongshu(string UserId, int VersionValue, int BatchNo, int Tongshu);
        public Task<string> UpdateShutting(string UserId, int VersionValue, int BatchNo, int OpenValue);
        public Task<string> UpdateGroupNo(string UserId, int VersionValue, int BatchNo, int GroupNo);
       
    }
}
