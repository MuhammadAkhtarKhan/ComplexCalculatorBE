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
        public Task<CalculatorModel> GetAllByUserId(string UserId);
        public Task<string> AddCalculation(CalculatorModel calculatorModel);
        public Task<string> AddMultiple(List<CalculatorModel> lstCalculations);
        public Task<CalculatorModel> GetLatest(string UserId);
        public Task<List<CalculatorSumModel>> GetAllSum(string UserId);
       
    }
}
