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

        public async Task<TotalScoreBoradModelResponse> GetDataTotalScoreBoardByGroupNoAndTipMode(int groupNo, int tipMode)
        {
            var response = new TotalScoreBoradModelResponse();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Execute the query and get the GridReader
            var multi = await connection.QueryMultipleAsync(
                "EXEC dbo.spGetDataTotalScoreBoard @GroupNo , @TipMode",
                new { GroupNo = groupNo, TipMode=tipMode }
            );

            try
            {
                // Read multiple-row admin calculations
                response.LstScoreBoardGrid = (await multi.ReadAsync<ScoreBoardGrid>()).ToList();
                
                // Read single-row summary grid data
                response.ScoreBoardTotal = await multi.ReadFirstOrDefaultAsync<ScoreBoardTotal>();
                               
            }
            finally
            {
                // Explicitly dispose of the GridReader after all data is read
                multi.Dispose();
            }

            return response;
       
        }

        public async Task<SummaryAndAdminCalculationsResponse> GetAdminSummaryAndDataByGroupNoAndTipMode(int groupNo, int? tipMode=5000)
        {
            var newList = new SummaryAndAdminCalculationsResponse();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Execute the query and get the GridReader
            var multi = await connection.QueryMultipleAsync(
                "EXEC dbo.GetSummaryAndGridDataByGroupNo @GroupNo, @TipMode",
                new { GroupNo = groupNo, TipMode=tipMode }
            );

            try
            {
                // Read single-row summary grid data
                newList.SummaryGrid = await multi.ReadFirstOrDefaultAsync<SummaryGridPannel>();

                // Read single-row summary data
                newList.Summary = await multi.ReadFirstOrDefaultAsync<CalculatorSumModel>();

                // Read multiple-row admin calculations
                newList.Calculations = (await multi.ReadAsync<AdminCalculations>()).ToList();

                // Read groups for drop down
                newList.Groups = (await multi.ReadAsync<GroupDropDown>())?.ToList() ?? new List<GroupDropDown>();
            }
            finally
            {
                // Explicitly dispose of the GridReader after all data is read
                multi.Dispose();
            }

            return newList;
        }
    }
}
