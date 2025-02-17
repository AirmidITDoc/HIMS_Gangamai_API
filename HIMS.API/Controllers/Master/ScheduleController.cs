﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.Users;
using HIMS.Model.Users;
using System.Collections.Generic;
using HIMS.Data.Master;
using HIMS.Model.Master;

namespace HIMS.API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly I_ScheduleMaster _I_ScheduleMaster;
        public ScheduleController(I_ScheduleMaster i_ScheduleMaster)
        {
            this._I_ScheduleMaster = i_ScheduleMaster;
        }
        [HttpGet]
        [Route("get-schedulers")]
        public IActionResult GetSchedulers(string ScheduleName)
        {
            return Ok(_I_ScheduleMaster.Get(ScheduleName));
        }
        [HttpPost]
        [Route("save")]
        public IActionResult Save(ScheduleMaster obj)
        {
            _I_ScheduleMaster.Insert(obj);
            obj.Id = 1;
            return Ok(obj);
        }
        [HttpDelete]
        [Route("remove-scheduler")]
        public IActionResult RemoveScheduler(int Id)
        {
            _I_ScheduleMaster.Delete(Id);
            return Ok();
        }
    }
}
