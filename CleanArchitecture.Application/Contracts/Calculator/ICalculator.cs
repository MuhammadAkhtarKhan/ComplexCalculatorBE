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
        
        public Task<List<CalculatorResponseModel>> GetAllByGroupNo(int GroupNo);
        public Task<List<CalculatorResponseModel>> GetAllEndThreadByGroupNo(int GroupNo);        
        public Task<List<CalculatorResponseModel>> AddMultiple(List<CalculatorResponseModel> lstCalculations);
        public Task<TempCalculatorResponseModel> AddOrUpdateTempCalculator(TempCalculatorResponseModel calculator);
        public Task<string>  DeleteTempCalculator(int id);
        public Task<string>  DeleteTempCalculatorByUserId(string userId);
        public Task<string> UpdateShuttingByGroupNo(int groupNo, int shutting);
        public Task<string> UpdateEndThreadByUserIdChangshiAndTongshu(string userId, int changshi, int tongshu);
        public Task<List<TempCalculatorResponseModel>> GetAllTempCalculatorByGroupNo(int groupNo);
        public Task<string> DeleteAllByUserId(string UserId);
        public Task<string> DeleteAllByNameAndUserId(string UserId, string name);
        public Task<string> DeleteAllByUserIdChangshiAndTongshu(string UserId, int changshi, int tongshu);
        public Task<string> DeleteById(int UserId);
        public Task<CalculatorResponseModel> GetLatest(string UserId);
        public Task<int> GetLatestBatchNo(string UserId);
        public Task<CalculatorResponse> GetAllSum(string UserId, int VersionValue, int BatchNo);
       
    }
}
