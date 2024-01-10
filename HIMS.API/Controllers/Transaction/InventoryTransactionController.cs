using Microsoft.AspNetCore.Mvc;
using HIMS.Model.Inventory;
using HIMS.Data.Inventory;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class InventoryTransactionController : Controller
    {
        /* public IActionResult Index()
         {
             return View();
         }*/

        public readonly I_Indent _indent;
        public readonly I_IssueTrackingInfo _IssueTrackingInfo;
    
        public InventoryTransactionController(
           I_Indent indent,
           I_IssueTrackingInfo issueTrackingInfo
        )
        {
            this._indent = indent;
            this._IssueTrackingInfo = issueTrackingInfo;
        }

        [HttpPost("IndentSave")]
        public IActionResult IndentSave(IndentParams indentParams)
        {
            var IndentInsert = _indent.Insert(indentParams);
            return Ok(IndentInsert);
        }

        [HttpPost("IndentUpdate")]
        public IActionResult IndentUpdate(IndentParams indentParams)
        {
            var IndentUpdate = _indent.Update(indentParams);
            return Ok(IndentUpdate);
        }

        [HttpPost("IssueTrackerSave")]
        public IActionResult IssueTrackerSave(IssueTrackerParams issueTrackerParams)
        {
            var IndentInsert = _IssueTrackingInfo.Insert(issueTrackerParams);
            return Ok(IndentInsert);
        }

        [HttpPost("IssueTrackerUpdate")]
        public IActionResult IssueTrackerUpdate(IssueTrackerParams issueTrackerParams)
        {
            var IndentUpdate = _IssueTrackingInfo.Update(issueTrackerParams);
            return Ok(IndentUpdate);
        }
        [HttpPost("IssueTrackerUpdateStatus")]
        public IActionResult IssueTrackerUpdateStatus(IssueTrackerParams issueTrackerParams)
        {
            var IndentUpdate = _IssueTrackingInfo.UpdateStatus(issueTrackerParams);
            return Ok(IndentUpdate);
        }
    }
}

