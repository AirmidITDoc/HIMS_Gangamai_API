using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Opd;
using HIMS.Model.Inventory;
using HIMS.Model.Transaction;
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
      
        public InventoryTransactionController(
           I_Indent indent
        )
        {
            this._indent = indent;
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

    }
}

