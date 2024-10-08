﻿using HIMS.Common.Extensions;
using HIMS.Model.Master;
using HIMS.Model.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Master
{
    public class R_ScheduleMaster : GenericRepository, I_ScheduleMaster
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public R_ScheduleMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<ScheduleMaster> Get(string ScheduleName)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@ScheduleName", string.IsNullOrWhiteSpace(ScheduleName) ? "" : ScheduleName);
            return GetList<ScheduleMaster>("SELECT * FROM ScheduleMaster WHERE IsActive=1 AND ScheduleName LIKE '%'+@ScheduleName+'%'", para);
        }

        public string Insert(ScheduleMaster obj)
        {
            SqlParameter[] para = new SqlParameter[10];
            if (!string.IsNullOrWhiteSpace(obj.Custom))
                para[0] = new SqlParameter("@Custom", obj.Custom) { Size = -1 };
            else
                para[0] = new SqlParameter("@Custom", DBNull.Value);
            if (!string.IsNullOrWhiteSpace(obj.Days))
                para[1] = new SqlParameter("@Days", obj.Days) { Size = 250 };
            else
                para[1] = new SqlParameter("@Days", DBNull.Value);
            para[2] = new SqlParameter("@Id", obj.Id);
            para[3] = new SqlParameter("@Query", obj.Query) { Size = -1 };

            if (!string.IsNullOrWhiteSpace(obj.Dates))
                para[4] = new SqlParameter("@Dates", obj.Dates) { Size = -1 };
            else
                para[4] = new SqlParameter("@Dates", DBNull.Value);
            if (obj.EndDate.HasValue)
                para[5] = new SqlParameter("@EndDate", obj.EndDate.Value);
            else
                para[5] = new SqlParameter("@EndDate", DBNull.Value);
            para[6] = new SqlParameter("@Hours", obj.Hours) { Size = -1 };
            para[7] = new SqlParameter("@ScheduleType", obj.ScheduleType);
            para[8] = new SqlParameter("@StartDate", obj.StartDate);
            para[9] = new SqlParameter("@ScheduleName", obj.ScheduleName);
            ExecuteObjectBySP("IUD_Scheduler", para);
            return "OK";
        }
        public string Delete(int Id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>
            {
                { "Id", Id }
            };
            ExecNonQuery("UPDATE ScheduleMaster SET IsActive=0 WHERE Id=@Id", dic);
            return "OK";
        }
    }
}
