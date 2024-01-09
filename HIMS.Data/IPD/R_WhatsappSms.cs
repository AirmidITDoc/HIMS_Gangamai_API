using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_WhatsappSms :GenericRepository,I_WhatsappSms
    {
        public R_WhatsappSms(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public string Insert(WhatsappSmsparam WhatsappSmsparam)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SMSOutGoingID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = WhatsappSmsparam.InsertWhatsappsmsInfo.ToDictionary();
            dic.Remove("SMSOutGoingID");
            dic.Remove("PatientType");
            var Id=ExecNonQueryProcWithOutSaveChanges("insert_WhatsappSmsinfo", dic, outputId1);



            _unitofWork.SaveChanges();
            return Id;
        }
    }
}
