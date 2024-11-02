using AutoMapper;
using ComplexCalculator.Application.Contracts.Calculator;
using ComplexCalculator.Application.Models;
using ComplexCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCalculator.Infrastructure.Services.CalculatorService
{
    public class CalculatorService : ICalculator
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CalculatorService(
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<string> AddCalculation(CalculatorResponseModel calculatorModel)
        {
            if (calculatorModel == null) { throw new ArgumentNullException(nameof(calculatorModel)); }
            try
            {
                Calculator calculator = _mapper.Map<Calculator>(calculatorModel);
                calculator.CreatedOn = DateTime.Now;
                await _context.Calculators.AddAsync(calculator);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return "Data added!";


        }
        public async Task<string> AddMultiple(List<CalculatorResponseModel> lstCalculation)
        {
            if (lstCalculation.Count < 0) { throw new ArgumentNullException(nameof(lstCalculation)); }
            try
            {
                List<Calculator> calculatons = _mapper.Map<List<Calculator>>(lstCalculation);
                foreach (Calculator calculator in calculatons)
                {
                    calculator.CreatedOn = DateTime.Now;
                    calculator.Changci = 1;
                    await _context.Calculators.AddAsync(calculator);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return "Data added!";


        }

        public Task<CalculatorResponseModel> GetAllByUserId(string UserId)
        {
            throw new NotImplementedException();
        }
        public async Task<CalculatorResponse> GetAllSum(string UserId, int VersionValue, int BatchNo)
        {
            CalculatorResponse calculatorResponse = new CalculatorResponse();
            List<CalculatorSumModel> calculatorSumModelsList = new List<CalculatorSumModel>();
          
            var result = await _context.Calculators
                  .Where(c => c.UserId == UserId && c.Version==VersionValue && c.BatchNo==BatchNo) // Filter by UserId
                  .GroupBy(t => t.UserId)
                             .Select(t => new
                             {
                                 One = t.Sum(d => d.One),
                                 Two = t.Sum(d => d.Two),
                                 Three = t.Sum(d => d.Three),
                                 Four = t.Sum(d => d.Four),
                                 Five = t.Sum(d => d.Five),
                                 Six = t.Sum(d => d.Six),
                                 Luozi = t.Sum(d => d.Luozi)
                             }).ToListAsync();

            foreach (var item in result)
            {
                calculatorSumModelsList.Add(new CalculatorSumModel
                {
                    One = item.One,
                    Two = item.Two,
                    Three = item.Three,
                    Four = item.Four,
                    Five = item.Five,
                    Six = item.Six,
                    Luozi = item.Luozi
                });
            }

            var resultRecords = await _context.Calculators
               .Where(c => c.UserId == UserId && c.Version == VersionValue && c.BatchNo == BatchNo) // Filter by UserId              
                          .Select(t => new GridCalculatorModel
                          {
                              Name = t.Name,
                              IdentifiedData = t.IdentifiedData,
                              Changci = t.Changci,
                              Tongshu = t.Tongshu                             
                          }).ToListAsync();
            
            calculatorResponse.calculatorResponse = calculatorSumModelsList;
            calculatorResponse.gridCalculatorModel=resultRecords;

            //List<CalculatorSumModel> calculatorRes = _mapper.Map<List<CalculatorSumModel>>(calculatorList);

            return calculatorResponse;

        }
          public async Task<string> UpdateTongshu(string UserId, int VersionValue, int BatchNo, int Tongshu)
        {

            try
            {
                var result = await _context.Calculators
                                 .Where(c => c.UserId == UserId && c.Version == VersionValue && c.BatchNo == BatchNo).ToListAsync(); // Filter by UserId

                // Update the Tongshu value for each item in the result
                if (result.Count == 0)
                {
                    return " No record found!";
                }
                foreach (var item in result)
                {
                    item.Tongshu = Tongshu;
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
           

            return "success";

        }

        public async Task<CalculatorResponseModel> GetLatest(string UserId)
        {
            if (UserId == null) { throw new ArgumentNullException(nameof(UserId)); }

            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not available.");
            }

            var result = await _context.Calculators.FirstOrDefaultAsync(x => x.UserId == UserId);
            // CalculatorResponseModel model = new CalculatorResponseModel();
            // if (result!=null)
            // {
            //     model.UserId = UserId;
            //     model.One = result.One;
            //     model.Two = result.Two;
            //     model.Three = result.Three;
            //     model.Four = result.Four;
            //     model.Five = result.Five;
            //     model.Luozi = result.Luozi;

            // }     

            // return model;
            CalculatorResponseModel calculatorModel = _mapper.Map<CalculatorResponseModel>(result);
            return calculatorModel;

        }
        public async Task<int> GetLatestBatchNo(string UserId)
        {
            if (UserId == null) { throw new ArgumentNullException(nameof(UserId)); }

            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not available.");
            }

            var latestBatchNo = await _context.Calculators
                            .Where(c => c.UserId == UserId)
                            .OrderByDescending(c => c.BatchNo)
                            .Select(c => c.BatchNo)
                            .FirstOrDefaultAsync();

            return latestBatchNo;

        }

    }
}
