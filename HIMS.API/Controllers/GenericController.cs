using HIMS.API.Utility;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;

        public GenericController(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        //Get
        [HttpPost]
        [Route("GetByProc")]
        public IActionResult Get(string procName, [FromBody] JObject entity)
        {
            var result = _genericRepository.ExecDataSetProc(procName, entity.ToDictionary());
            return Ok(result);
        }

        //Get
        [HttpPost]
        [Route("GetBySelectQuery")]
        public IActionResult GetBySelectQuery(string query, [FromBody] JObject entity)
        {
            var result = _genericRepository.ExecDataSetQuery(query, entity.ToDictionary());
            return Ok(result);
        }

        //Get
        [HttpPost]
        [Route("ExecByQueryStatement")]
        public IActionResult ExecByQueryStatement(string query, [FromBody] JObject entity)
        {
            var result = _genericRepository.ExecNonQuery(query, entity.ToDictionary());
            return Ok(result);
        }


        //Get-DataTable
        [HttpPost]
        [Route("GetAll")]
        public IActionResult Get([FromBody] JArray entity)
        {
            var result = _genericRepository.ExecDataSetProcWithDataTable("spGetTableData", entity);
            return Ok(result);
        }

        //Add-Update-Delete
        [HttpPost]
        public IActionResult Post(string procName, [FromBody] JObject entity)
        {
            var result = _genericRepository.ExecNonQueryProc(procName, entity.ToDictionary());
            return Ok(result);
        }

        //bulk Add-Update-Delete
        [HttpPost("PostBulk")]
        public IActionResult PostBulk(string procName, [FromBody] JArray entity)
        {
            var result = _genericRepository.ExecNonQueryProcBulk(procName, entity);
            return Ok(result);
        }

        //Delete
        //[HttpDelete]
        //public IActionResult Delete(string procName, [FromBody] JObject entity)
        //{
        //    var result = _genericRepository.ExecNonQueryProc(procName, entity.ToDictionary());
        //    return Ok(result);
        //}

        //bulk delete
        //[HttpDelete("DeleteBulk")]
        //public IActionResult DeleteBulk(string procName, [FromBody] JArray entity)
        //{
        //    var result = _genericRepository.ExecNonQueryProcBulk(procName, entity);
        //    return Ok(result);
        //}

        //[HttpPost]
        //[Route("BindDropdown")]
        //public IActionResult BindDropdown([FromBody] JArray entity)
        //{
        //    var result = _genericRepository.ExecDataSetProc("spBindDropdown", entity);
        //    return Ok(result);
        //}
    }
}
