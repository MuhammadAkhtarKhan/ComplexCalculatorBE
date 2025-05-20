using ComplexCalculator.Application.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;

namespace ComplexCalculator.API.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;

        public AdminController(IAdmin admin)
        {
            this._admin = admin;
        }

        [HttpGet("GetAdminSummaryAndDataByGroupNoAndTipMode")]
        public async Task<IActionResult> GetAdminSummaryAndDataByGroupNoAndTipMode(int groupNo, int? tipMode=5000)
        {
            var result = await this._admin.GetAdminSummaryAndDataByGroupNoAndTipMode(groupNo,tipMode);
            return Ok(result);
        }

        [HttpGet("GetDataTotalScoreBoardByGroupNoAndTipMode")]
        public async Task<IActionResult> GetDataTotalScoreBoardByGroupNoAndTipMode(int groupNo, int tipMode)
        {
            var result = await this._admin.GetDataTotalScoreBoardByGroupNoAndTipMode(groupNo,tipMode);
            return Ok(result);
        } 
        [HttpGet("GetDataPerRoundByGroupNoAndTipMode")]
        public async Task<IActionResult> GetDataPerRoundByGroupNoAndTipMode(int groupNo, int tipMode)
        {
            var result = await this._admin.GetDataPerRoundByGroupNoAndTipMode(groupNo,tipMode);
            return Ok(result);
        }
    }
}
