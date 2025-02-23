﻿using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Master.Billing
{ 
   public class R_ServiceMaster :GenericRepository ,I_ServiceMaster
    {
        public R_ServiceMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
        public bool SaveService(ServiceTarriffParams ServiceTarriffParams)
        {
            // throw new NotImplementedException();
            var disc = ServiceTarriffParams.SaveServiceTarriffParams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Assign_Servicesto_DifferTraiff", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool Save(ServiceMasterParams ServiceMasterParams)
        {
            //Service master insert
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ServiceId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = ServiceMasterParams.ServiceMasterInsert.ToDictionary();
            dic.Remove("ServiceId");
            var ServiceId = ExecNonQueryProcWithOutSaveChanges("m_insert_ServiceMaster_1", dic, outputId);
            
            
            // Service Detail Insert
            foreach(var a in ServiceMasterParams.ServiceDetailInsert)
            {
                var d = a.ToDictionary();
                d["ServiceId"] = ServiceId;
                ExecNonQueryProcWithOutSaveChanges("m_insert_ServiceDetail_1", d);

            }
            _unitofWork.SaveChanges();
            return true;
        }

          public bool Update(ServiceMasterParams ServiceMasterParams)
           {

            //Update Service
            var dic = ServiceMasterParams.ServiceMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_ServiceMaster_1", dic);

            //Delete Service Details
            var S_Det = ServiceMasterParams.ServiceDetDelete.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Delete_M_ServiceDetail", S_Det);

            //add ServiceDetails
            foreach (var a in ServiceMasterParams.ServiceDetailInsert)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_ServiceDetail_1", disc);
            }

            _unitofWork.SaveChanges();
            return true;

        }

    }
}
