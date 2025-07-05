using AutoMapper;
using ComplexCalculator.Application.Contracts.Calculator;
using ComplexCalculator.Application.Models;
using ComplexCalculator.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComplexCalculator.Infrastructure.Services.CalculatorService
{
    public class CalculatorService : ICalculator
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _connectionString;
        public CalculatorService(
            ApplicationDbContext context,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            this._context = context;
            this._mapper = mapper;
            this._connectionString = configuration.GetConnectionString("DefaultConnection");
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
        public async Task<string> DeleteAllByUserIdChangshiAndTongshu(string userId, int changshi, int tongshu)
        {
            if (userId == null) { throw new ArgumentNullException(nameof(userId)); }
            try
            {
                var result = await _context.Calculators
                    .Where(c => c.UserId == userId && c.Changci==changshi && c.Tongshu==tongshu)
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

        public async Task<TempCalculatorResponseModel> AddOrUpdateTempCalculator(TempCalculatorResponseModel calc)
        {
            try
            {
                TempCalculator entity;

                // Check if the record exists
                var existing = await _context.TempCalculators.FirstOrDefaultAsync(x => x.Id == calc.Id);

                if (existing != null)
                {
                    // Update existing entity
                    _mapper.Map(calc, existing);
                    entity = existing;
                }
                else
                {
                    // Add new entity
                    entity = _mapper.Map<TempCalculator>(calc);
                    await _context.TempCalculators.AddAsync(entity);
                }

                await _context.SaveChangesAsync();

                // Re-fetch to ensure you get DB-generated fields (e.g., Ids, timestamps)
                var updatedEntity = await _context.TempCalculators.FirstOrDefaultAsync(x => x.Id == entity.Id);

                return _mapper.Map<TempCalculatorResponseModel>(updatedEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AddOrUpdateTempCalculator: {ex.Message}", ex);
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
        public async Task<string> UpdateShuttingByGroupNo(int groupNo, int shutting)
        {
            try
            {
                // 1. Retrieve all entries with the given GroupNo
                List<Calculator> allCalculations = await _context.Calculators
                    .Where(x => x.GroupNo == groupNo)
                    .ToListAsync();

                // 2. Update each entity's Shutting value
                foreach (var calc in allCalculations)
                {
                    calc.Shutting = shutting; // Or apply your custom logic
                }

                // 3. Save changes to the database
                await _context.SaveChangesAsync();

                // 4. Map to response models and return
                return "sucess";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UpdateShuttingByGroupNo: {ex.Message}");
            }
        }    
        public async Task<string> UpdateEndThreadByUserIdChangshiAndTongshu(string userId, int changci, int tongshu)
        {
            var response = "";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();            
            try
            {
                // Execute the query and get the GridReader
                var rowsAffected = await connection.ExecuteAsync(
                         "UPDATE Calculators SET EndThread = 1 WHERE UserId = @UserId and Changci=@Changci and Tongshu=@Tongshu;",
                         new { UserId = userId, Changci =changci, Tongshu=tongshu}
                     );

                // Optionally check if the update was successful
                if (rowsAffected > 0)
                {
                    response = "success";
                }
                else
                {
                    response = "no_row_to_update";
                }

            }
            catch (Exception)
            {

                throw;
            }           
            finally
            {
                // Explicitly dispose of the GridReader after all data is read
                connection.Dispose();
            }

            return response;
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
