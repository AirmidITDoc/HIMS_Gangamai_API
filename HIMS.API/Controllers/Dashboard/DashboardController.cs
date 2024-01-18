using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.Users;
using HIMS.Model.Users;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using HIMS.Data.Dashboard;
using HIMS.API.Utility;

namespace HIMS.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly I_Dashboard i_Dashboard;
        public DashboardController(I_Dashboard _Dashboard)
        {
            this.i_Dashboard = _Dashboard;
        }
        [HttpPost]
        [Route("get-pie-chart-date")]
        public IActionResult Get(string procName, [FromBody] JObject entity)
        {
            var result = i_Dashboard.GetPieChartData(procName, entity.ToDictionary());
            return Ok(result);
        }
    }
}
