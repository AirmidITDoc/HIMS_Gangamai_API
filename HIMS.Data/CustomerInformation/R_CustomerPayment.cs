using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Data.CustomerPayment;
using HIMS.Common.Utility;
using HIMS.Data.CustomerAMCInfo;

namespace HIMS.Data.CustomerInformation
{
    public class R_CustomerAMCInfo : GenericRepository , I_CustomerAMCInfo
    {
        public R_CustomerAMCInfo(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string CustomerAMCInsert(CustomerAmcParams customerAmcParams)
        {
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AMCId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = customerAmcParams.AmcSaveParams.ToDictionary();
            disc3.Remove("AMCId");
            var vPaymentId = ExecNonQueryProcWithOutSaveChanges("m_insert_CustomerAMC_1", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return vPaymentId;
        }
        public bool CustomerAMCUpdate(CustomerAmcParams customerAmcParams)
        {

            var disc3 = customerAmcParams.CustomerAmcUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_CustomerAMC", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool CustomerAMCCancel(CustomerAmcParams customerAmcParams)
        {

            var disc3 = customerAmcParams.CustomerAmcCancel.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_CustomerAMC_Cancel", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
