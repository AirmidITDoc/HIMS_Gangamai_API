﻿using System.Data.SqlClient;
using System.Data;
using HIMS.Common.Utility;
using HIMS.Model.CustomerInformation;
using HIMS.Data.CustomerInformation;

namespace HIMS.Data.CustomerInformation
{
    public class R_CustomerInformation : GenericRepository, I_CustomerInformation
    {
        public R_CustomerInformation(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool SaveVendorInformation(VendorInformationParam VendorInformationParam)
        {
            // throw new NotImplementedException();
            var disc = VendorInformationParam.SaveVendorInformationParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_VendorInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateVendorInformation(VendorInformationParam VendorInformationParam)
        {
            // throw new NotImplementedException();
            var disc = VendorInformationParam.UpdateVendorInformationParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_VendorInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public string CustomerInformationInsert(CustomerInformationParams customerInformationParams)
        {
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@CustomerId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = customerInformationParams.CustomerInformationInsert.ToDictionary();
            disc3.Remove("CustomerId");
            var vCustomerId = ExecNonQueryProcWithOutSaveChanges("m_insert_CustomerInformation", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return vCustomerId;
        }
        public bool CustomerInformationUpdate(CustomerInformationParams customerInformationParams)
        {
           
            var disc3 = customerInformationParams.CustomerInformationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_CustomerInformation", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
