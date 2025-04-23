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
        // write a method to delete all data by userId and return the success message
        public async Task<string> DeleteAllByUserId(string UserId)
        {
            if (UserId == null) { throw new ArgumentNullException(nameof(UserId)); }
            try
            {
                var result = await _context.Calculators
                    .Where(c => c.UserId == UserId)
                    .ToListAsync();
                if (result.Count == 0)
                {
                    return "No record found!";
                }
                _context.Calculators.RemoveRange(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return "Data deleted successfully!";
        }
        public async Task<List<CalculatorResponseModel>> AddMultiple(List<CalculatorResponseModel> lstCalculation)
        {
            if (lstCalculation == null || lstCalculation.Count <= 0)
                throw new ArgumentNullException(nameof(lstCalculation));

            try
            {
                // Map input to entity list
                List<Calculator> calculations = _mapper.Map<List<Calculator>>(lstCalculation);
                var userIds = calculations.Select(c => c.UserId).Distinct().ToList();
                var ids = calculations.Select(c => c.Id).Distinct().ToList();

                // Query existing records in one call
                var existingRecords = await _context.Calculators
                    .Where(c => userIds.Contains(c.UserId) && ids.Contains(c.Id))
                    .ToListAsync();

                var resultList = new List<Calculator>();

                foreach (var calc in calculations)
                {
                    var existing = existingRecords
                        .FirstOrDefault(c => c.UserId == calc.UserId && c.Id == calc.Id);

                    if (existing != null)
                    {
                        // Update existing
                        existing.Tongshu = calc.Tongshu;
                        existing.InputByUser = calc.InputByUser;
                        existing.GroupNo = calc.GroupNo;
                        existing.Changci = calc.Changci;
                        existing.Shutting = calc.Shutting;
                        existing.EndThread = calc.EndThread;
                        existing.WinOrLose = calc.WinOrLose;
                        existing.MainTube = calc.MainTube;
                        existing.IsPrevious = true;

                        resultList.Add(existing);
                    }
                    else
                    {
                        // New record
                        calc.CreatedOn = DateTime.Now;
                        calc.Changci = 1;

                        _context.Calculators.Add(calc); // no await needed here
                        resultList.Add(calc);
                    }
                }

                await _context.SaveChangesAsync();

                return _mapper.Map<List<CalculatorResponseModel>>(resultList);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AddMultiple: {ex.Message}");
            }
        }

        //public async Task<List<CalculatorResponseModel>> AddMultiple(List<CalculatorResponseModel> lstCalculation)
        //{
        //    if (lstCalculation == null || lstCalculation.Count <= 0)
        //        throw new ArgumentNullException(nameof(lstCalculation));

        //    var resultList = new List<Calculator>();

        //    try
        //    {
        //        List<Calculator> calculations = _mapper.Map<List<Calculator>>(lstCalculation);

        //        foreach (Calculator calc in calculations)
        //        {
        //            // Check by UserId and Id
        //            var existingCalc = await _context.Calculators
        //                .FirstOrDefaultAsync(c => c.UserId == calc.UserId && c.Id == calc.Id);

        //            if (existingCalc != null)
        //            {
        //                // Update existing entry
        //                existingCalc.Tongshu = calc.Tongshu;
        //                existingCalc.InputByUser = calc.InputByUser;
        //                existingCalc.GroupNo = calc.GroupNo;
        //                existingCalc.Changci = calc.Changci;
        //                existingCalc.Shutting = calc.Shutting;
        //                existingCalc.EndThread = calc.EndThread;
        //                existingCalc.WinOrLose = calc.WinOrLose;
        //                existingCalc.MainTube = calc.MainTube;
        //                existingCalc.IsPrevious = true;

        //                resultList.Add(existingCalc);
        //            }
        //            else
        //            {
        //                // Add new entry
        //                calc.CreatedOn = DateTime.Now;
        //                calc.Changci = 1;

        //                await _context.Calculators.AddAsync(calc);
        //                resultList.Add(calc);
        //            }
        //        }

        //        await _context.SaveChangesAsync();

        //        // Map back to response model to return
        //        return _mapper.Map<List<CalculatorResponseModel>>(resultList);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error in AddMultiple: {ex.Message}");
        //    }
        //}


        public Task<List<CalculatorResponseModel>> GetAll()
        {
            var result = _context.Calculators.ToList();
            return Task.FromResult(_mapper.Map<List<CalculatorResponseModel>>(result));
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
           

            return "Data Updated Successfully!";

        }
         public async Task<string> UpdateShutting(string UserId, int VersionValue, int BatchNo, int OpentValue)
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
                    item.Shutting = OpentValue;
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
           

            return "Shutting updated successfully!";

        }
        public async Task<string> UpdateGroupNo(string UserId, int VersionValue, int BatchNo, int GroupNo)
        {

            try
            {
                var result = await _context.Calculators
                                 .Where(c => c.UserId == UserId && c.Version == VersionValue && c.BatchNo == BatchNo)
                                 .ToListAsync();

                if (result.Count == 0)
                {
                    return " No record found!";
                }
                foreach (var item in result)
                {
                    item.GroupNo = GroupNo;
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
           

            return "Group number updated successfully!";

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
