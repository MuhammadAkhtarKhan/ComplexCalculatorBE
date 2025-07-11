using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCalculator.Application.Models.Admin;

namespace ComplexCalculator.Application.Contracts.Admin
{
    public interface IAdmin
    {
        public Task<SummaryAndAdminCalculationsResponse> GetAdminSummaryAndDataByGroupNoAndTipMode(int groupNo,int? tipMode);
        public Task<TotalScoreBoradModelResponse> GetDataTotalScoreBoardByGroupNoAndTipMode(int groupNo, int tipMode);
        public Task<List<DataPerRoundSum>> GetDataPerRoundByGroupNoAndTipMode(int groupNo, int tipMode);
        public Task<string> UpdateDataByShuttingAndGroupNo(int groupNo, int shutting);
        public Task<string> ClearAllDataEndThreadOneByGroupNo(int groupNo);
    }
}
