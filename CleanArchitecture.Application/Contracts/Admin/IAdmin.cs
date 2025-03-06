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
        public Task<SummaryAndAdminCalculationsResponse> GetSummaryAndDataByGroupNo(int GroupNo);
    }
}
