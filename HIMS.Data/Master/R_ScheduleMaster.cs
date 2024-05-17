using HIMS.Common.Extensions;
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

        public List<ScheduleMaster> Get()
        {
            return GetList<ScheduleMaster>("SELECT * FROM ScheduleMaster WHERE IsActive=1", new SqlParameter[0]);
        }

        public string Insert(ScheduleMaster obj)
        {
            SqlParameter[] para = new SqlParameter[10];
            para[0] = new SqlParameter("@Custom", obj.Custom) { Size = -1 };
            para[1] = new SqlParameter("@Days", obj.Days) { Size = 250 };
            para[2] = new SqlParameter("@Id", obj.Id);
            para[3] = new SqlParameter("@Query", obj.Query) { Size = -1 };
            para[4] = new SqlParameter("@Dates", obj.Dates) { Size = -1 };
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
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Id", Id);
            GetList<ScheduleMaster>("UPDATE ScheduleMaster SET IsActive=0 WHERE Id=@Id", para);
            return "OK";
        }
    }
}
