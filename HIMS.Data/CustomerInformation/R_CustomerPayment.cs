using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Data.CustomerPayment;
using HIMS.Common.Utility;

namespace HIMS.Data.CustomerInformation
{
    public class R_CustomerPayment:GenericRepository , I_CustomerPayments
    {
        public R_CustomerPayment(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string CustomerPaymentInsert(CustomerPaymentParams CustomerPaymentParams)
        {
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = CustomerPaymentParams.CustomerPaymentInsert.ToDictionary();
            disc3.Remove("PaymentId");
            var vPaymentId = ExecNonQueryProcWithOutSaveChanges("m_insert_CustomerPayment", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return vPaymentId;
        }
        public bool CustomerPaymentUpdate(CustomerPaymentParams CustomerPaymentParams)
        {

            var disc3 = CustomerPaymentParams.CustomerPaymentUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_CustomerPayment", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
