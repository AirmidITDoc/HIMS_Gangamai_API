using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Common.Utility;

namespace HIMS.Data.Inventory
{
    public class R_SMSConfig: GenericRepository, I_SMS_Config
    {
        public R_SMSConfig(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string InsertSMSConfig(SMS_ConfigParam SMS_ConfigParam)
        {
            // throw new NotImplementedException();
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@ConfigId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = SMS_ConfigParam.InsertSMSConfigParam.ToDictionary();
            disc3.Remove("ConfigId");
            var vUrl = ExecNonQueryProcWithOutSaveChanges("M_insert_SMS_Config", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return vUrl;
        }
        public bool UpdateSMSConfigParam(SMS_ConfigParam SMS_ConfigParam) 
        {

            var disc3 = SMS_ConfigParam.updateSMSConfigParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_SMS_Config", disc3);

            _unitofWork.SaveChanges();
            return true;
        }




    }
}


    


    

