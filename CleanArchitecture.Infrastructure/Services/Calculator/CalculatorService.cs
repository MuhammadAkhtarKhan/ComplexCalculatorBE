﻿using AutoMapper;
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
        public async Task<string> DeleteAllByNameAndUserId(string userId, string name)
        {
            if (userId == null) { throw new ArgumentNullException(nameof(userId)); }
            try
            {
                var result = await _context.Calculators
                    .Where(c => c.UserId == userId && c.Name==name)
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
        public async Task<string> DeleteById(int Id)
        {
            if (Id == null) { throw new ArgumentNullException(nameof(Id)); }
            try
            {
                var result = await _context.Calculators
                    .Where(c => c.Id == Id)
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
                        existing.A = calc.A;
                        existing.B = calc.B;
                        existing.C = calc.C;
                        existing.D = calc.D;
                        existing.E = calc.E;
                        existing.One = calc.One;
                        existing.Two = calc.Two;
                        existing.Three = calc.Three;
                        existing.Four = calc.Four;
                        existing.Five = calc.Five;
                        existing.Six = calc.Six;
                        existing.Luozi = calc.Luozi;
                        existing.Format = calc.Format;
                        existing.EntryNo = calc.EntryNo;
                        existing.RawData = calc.RawData;
                        existing.IdentifiedData = calc.IdentifiedData;
                        existing.AccumulatedRawData = calc.AccumulatedRawData;

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

        public async Task<List<TempCalculatorResponseModel>> AddOrUpdateTempCalculator(TempCalculatorResponseModel calc)
        {
            try
            {
                // Check if the record exists (based on Id or other unique field)
                var existing = await _context.TempCalculators.FirstOrDefaultAsync(x => x.Id == calc.Id);

                if (existing != null)
                {
                    // Update existing entity
                    _mapper.Map(calc, existing); // Map new values onto existing entity
                    _context.TempCalculators.Update(existing); // Optional, EF tracks changes
                }
                else
                {
                    // Add new entity
                    TempCalculator newCalculation = _mapper.Map<TempCalculator>(calc);
                    await _context.TempCalculators.AddAsync(newCalculation);
                }

                await _context.SaveChangesAsync();

                // Return full list
                var allCalculations = await _context.TempCalculators.ToListAsync();
                return _mapper.Map<List<TempCalculatorResponseModel>>(allCalculations);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AddOrUpdateTempCalculator: {ex.Message}");
            }
        }

        public async Task<List<TempCalculatorResponseModel>> GetAllTempCalculatorByGroupNo(int groupNo)
        {
            try
            {                
                List<TempCalculator> allCalculations = await _context.TempCalculators.Where(x=>x.GroupNo==groupNo).ToListAsync();
                // 3. Map the list to response models and return
                return _mapper.Map<List<TempCalculatorResponseModel>>(allCalculations);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetAllTempCalculatorByGroupNo: {ex.Message}");
            }
        }

        public async Task<string> DeleteTempCalculator(int id)
        {
            if (id == null) { throw new ArgumentNullException(nameof(id)); }
            string res = "";
            try
            {
                // Find the existing entity by its primary key
                var existingEntity = await _context.TempCalculators
                    .FindAsync(id); // Use the correct primary key property

                if (existingEntity != null)
                {
                    _context.TempCalculators.Remove(existingEntity);
                    await _context.SaveChangesAsync();
                    res = "Deleted";
                }
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeleteTempCalculator: {ex.Message}");
            }
        }
        public async Task<string> DeleteTempCalculatorByUserId(string userId)
        {
            if (userId == null) { throw new ArgumentNullException(nameof(userId)); }
            try
            {
                var result = await _context.TempCalculators
                    .Where(c => c.UserId == userId)
                    .ToListAsync();
                if (result.Count == 0)
                {
                    return "No record found!";
                }
                _context.TempCalculators.RemoveRange(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return "Data deleted successfully!";
        }


        public Task<List<CalculatorResponseModel>> GetAll()
        {
            var result = _context.Calculators.ToList();
            return Task.FromResult(_mapper.Map<List<CalculatorResponseModel>>(result));
        }
        // write a method to get all data by EndThread is true  
        public Task<List<CalculatorResponseModel>> GetAllEndThreadByGroupNo(int GroupNo)
        {
            var result = _context.Calculators.Where(c => c.EndThread == true && c.GroupNo == GroupNo).ToList();
            return Task.FromResult(_mapper.Map<List<CalculatorResponseModel>>(result));
        }
        // write a method to get all data by EndThread is true  
        public Task<List<CalculatorResponseModel>> GetAllByGroupNo(int GroupNo)
        {
            var result = _context.Calculators.Where(c => c.GroupNo == GroupNo && c.EndThread == false).ToList();
            return Task.FromResult(_mapper.Map<List<CalculatorResponseModel>>(result));
        }
        public async Task<CalculatorResponse> GetAllSum(string UserId, int VersionValue, int BatchNo)
        {
            CalculatorResponse calculatorResponse = new CalculatorResponse();
            List<CalculatorSumModel> calculatorSumModelsList = new List<CalculatorSumModel>();

            var result = await _context.Calculators
                  .Where(c => c.UserId == UserId && c.Version == VersionValue && c.BatchNo == BatchNo) // Filter by UserId
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
                    One = item.One ?? 0,
                    Two = item.Two ?? 0,
                    Three = item.Three ?? 0,
                    Four = item.Four ?? 0,
                    Five = item.Five ?? 0,
                    Six = item.Six ?? 0,
                    Luozi = item.Luozi ?? 0
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
            calculatorResponse.gridCalculatorModel = resultRecords;

            //List<CalculatorSumModel> calculatorRes = _mapper.Map<List<CalculatorSumModel>>(calculatorList);

            return calculatorResponse;

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

            return latestBatchNo ?? 0; // Use null-coalescing operator to handle nullable int  
        }

       
    }
}
