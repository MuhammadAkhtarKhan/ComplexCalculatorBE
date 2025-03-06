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

        [HttpGet("GetSummayAndDataByGroupNo/{GroupNo}")]
        public async Task<IActionResult> GetSummayAndDataByGroupNo(int GroupNo)
        {
            var result = await this._admin.GetSummaryAndDataByGroupNo(GroupNo);
            return Ok(result);
        }
    }
}
