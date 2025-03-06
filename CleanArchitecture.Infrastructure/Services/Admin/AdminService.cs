using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using ComplexCalculator.Application.Contracts.Admin;
using ComplexCalculator.Application.Models;
using ComplexCalculator.Application.Models.Admin;
using ComplexCalculator.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ComplexCalculator.Infrastructure.Services.Admin
{
    public class AdminService : IAdmin
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;       
        private readonly string _connectionString;

        public AdminService(ApplicationDbContext context, IMapper mapper,IConfiguration configuration)
        {
            this._context = context;
            this._mapper = mapper;
            this._connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<SummaryAndAdminCalculationsResponse> GetSummaryAndDataByGroupNo(int GroupNo)
        {
            var newList = new SummaryAndAdminCalculationsResponse();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var multi = await connection.QueryMultipleAsync(
                "EXEC dbo.GetSummaryAndGridDataByGroupNo @GroupNo",
                new { GroupNo = GroupNo }
            );

            var summary = await multi.ReadFirstOrDefaultAsync<CalculatorSumModel>();
            var calculations = (await multi.ReadAsync<AdminCalculations>()).ToList();
            newList.Summary = summary;
            newList.Calculations = calculations;
            return newList;
            
        }
    }
}
