using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCalculator.Application.Contracts.Calculator;
using ComplexCalculator.Application.Models;
using ComplexCalculator.Domain.Entities;
using ComplexCalculator.Infrastructure.Services.CalculatorService;
using FluentAssertions;
using Moq;

namespace ComplexCalculator.Application.Tests.UseCases
{
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;

    using ComplexCalculator.Infrastructure;

    public class CalculatorServiceTests
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public CalculatorServiceTests()
        {
            // Setup AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CalculatorResponseModel, Calculator>();
            });
            _mapper = config.CreateMapper();

            // Setup InMemory DbContext
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CalculatorTestDb")
                .Options;
            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddCalculation_Should_Add_Record_And_Return_Message()
        {
            // Arrange
            var service = new CalculatorService(_context, _mapper);
            var model = new CalculatorResponseModel
            {
                UserId = "user1",
                One = 50,
                Two = 50,
                Three = 50,
                Four = 50,
                Five = 50,
                Six = 50,
                InputByUser="1"

            };

            // Act
            var result = await service.AddCalculation(model);

            // Assert
            Assert.Equal("Data added!", result);
            Assert.Equal(1, await _context.Calculators.CountAsync());
        }

        [Fact]
        public async Task DeleteAllByUserId_Should_Delete_User_Data()
        {
            _context.Calculators.RemoveRange(_context.Calculators);
            await _context.SaveChangesAsync();
            // Arrange
            _context.Calculators.Add(new Calculator
            {
                UserId = "user1",
                One = 50,
                Two = 50,
                Three = 50,
                Four = 50,
                Five = 50,
                Six = 50,
                InputByUser = "1"
            });
            _context.Calculators.Add(new Calculator
            {
                UserId = "user2",
                One = 50,
                Two = 50,
                Three = 50,
                Four = 50,
                Five = 50,
                Six = 50,
                InputByUser = "1"
            });
            _context.Calculators.Add(new Calculator
            {
                UserId = "user3",
                One = 50,
                Two = 50,
                Three = 50,
                Four = 50,
                Five = 50,
                Six = 50,
                InputByUser = "1"
            });
            await _context.SaveChangesAsync();

            var service = new CalculatorService(_context, _mapper);

            // Act
            var result = await service.DeleteAllByUserId("user3");

            // Assert
            Assert.Equal("Data deleted successfully!", result);
            Assert.Equal(2, await _context.Calculators.CountAsync());
        }

        //[Fact]
        //public async Task GetAllSum_Should_Return_Correct_Sums()
        //{
        //    // Arrange
        //    _context.Calculators.AddRange(new[]
        //    {
        //    new Calculator { UserId = "user1", Version = 1, BatchNo = 1, Two = 10 },
        //    new Calculator { UserId = "user1", Version = 1, Name = "A", Two = 20 },
        //    new Calculator { UserId = "user1", Version = 1, Name = "B", Two = 30 }
        //});
        //    await _context.SaveChangesAsync();

        //    var service = new CalculatorService(_context, _mapper);

        //    // Act
        //    var result = await service.GetAllSum("user1", 1, 1);

        //    // Assert
        //    Assert.Equal(1, result.);
        //    Assert.Equal(1, result[0].BatchNo);
        //    Assert.Equal(30, result[0].BatchNo);
        //}
    }


}
