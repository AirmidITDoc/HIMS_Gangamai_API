using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_DynamicExecuteSchedule :GenericRepository,I_DynamicExecuteSchedule
    {

        public R_DynamicExecuteSchedule(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(DynamicExecuteScheduleparam DynamicExecuteScheduleparam)
        {
           // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ScheduleId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = DynamicExecuteScheduleparam.InsertDynamicExecuteSchedule.ToDictionary();
            disc1.Remove("ScheduleId");
            var ScheduleId = ExecNonQueryProcWithOutSaveChanges("Inser_DynamicExecuteSchedule", disc1, outputId1);


            _unitofWork.SaveChanges();
            return ScheduleId;

        }

        public bool Update(DynamicExecuteScheduleparam DynamicExecuteScheduleparam)
        {
          //  throw new NotImplementedException();
          
            var disc2 = DynamicExecuteScheduleparam.UpdateDynamicExecuteSchedule.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_DynamicExecuteSchedule", disc2);


            _unitofWork.SaveChanges();
            return true;

        }
    }
}
