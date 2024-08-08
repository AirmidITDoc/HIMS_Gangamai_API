using HIMS.Data.CustomerPayment;
using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Model.Inventory;
using HIMS.Common.Utility;

namespace HIMS.Data.Inventory
{
    public  class R_Constants : GenericRepository, I_Constants
    {
        public R_Constants(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string InsertConstantsParam(constantsParams ConstantsParam)
        {
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ConstantId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = ConstantsParam.InsertConstantsParam.ToDictionary();
            disc3.Remove("ConstantId");
            var vConstantId = ExecNonQueryProcWithOutSaveChanges("M_insert_M_Constants", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return vConstantId;
        }
        public bool UpdateConstantsParam(constantsParams ConstantsParam)
        {

            var disc3 = ConstantsParam.updateConstantsParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("M_Update_M_Constants", disc3);

            _unitofWork.SaveChanges();
            return true;
        }


    }
}


    

