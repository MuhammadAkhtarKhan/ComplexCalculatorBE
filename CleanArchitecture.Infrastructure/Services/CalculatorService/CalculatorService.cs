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
        public async Task<string> AddCalculation(CalculatorModel calculatorModel)
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
        public async Task<string> AddMultiple(List<CalculatorModel> lstCalculation)
        {
            if (lstCalculation.Count < 0) { throw new ArgumentNullException(nameof(lstCalculation)); }
            try
            {
                List<Calculator> calculatons = _mapper.Map<List<Calculator>>(lstCalculation);
                foreach (Calculator calculator in calculatons)
                {
                    calculator.CreatedOn = DateTime.Now;
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

        public Task<CalculatorModel> GetAllByUserId(string UserId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<CalculatorSumModel>> GetAllSum(string UserId)
        {
            List<Calculator> calculatorList = new List<Calculator>();
          
            var result = await _context.Calculators
                  .Where(c => c.UserId == UserId) // Filter by UserId
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
                calculatorList.Add(new Calculator
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
            List<CalculatorSumModel> calculatorRes = _mapper.Map<List<CalculatorSumModel>>(calculatorList);

            return calculatorRes ?? new List<CalculatorSumModel>();

        }

        public async Task<CalculatorModel> GetLatest(string UserId)
        {
            if (UserId == null) { throw new ArgumentNullException(nameof(UserId)); }

            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not available.");
            }

            var result = await _context.Calculators.FirstOrDefaultAsync(x => x.UserId == UserId);
            // CalculatorModel model = new CalculatorModel();
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
            CalculatorModel calculatorModel = _mapper.Map<CalculatorModel>(result);
            return calculatorModel;

        }
    }
}
