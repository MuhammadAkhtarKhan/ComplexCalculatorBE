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
       
        //public Task<List<CalculatorResponseModel>> GetAll();
        public Task<List<CalculatorResponseModel>> GetAllByGroupNo(int GroupNo);
        public Task<List<CalculatorResponseModel>> GetAllEndThreadByGroupNo(int GroupNo);        
        public Task<List<CalculatorResponseModel>> AddMultiple(List<CalculatorResponseModel> lstCalculations);
        public Task<List<TempCalculatorResponseModel>> AddTempCalculator(TempCalculatorResponseModel calculator);
        public Task  DeleteTempCalculator(TempCalculatorResponseModel calculator);
        public Task<List<TempCalculatorResponseModel>> GetAllTempCalculatorByGroupNo(int groupNo);
        public Task<string> DeleteAllByUserId(string UserId);
        public Task<string> DeleteById(int UserId);
        public Task<CalculatorResponseModel> GetLatest(string UserId);
        public Task<int> GetLatestBatchNo(string UserId);
        public Task<CalculatorResponse> GetAllSum(string UserId, int VersionValue, int BatchNo);
       
    }
}
