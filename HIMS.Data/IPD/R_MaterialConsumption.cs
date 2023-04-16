using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_MaterialConsumption : GenericRepository, I_MaterialConsumption
    {
        public R_MaterialConsumption(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public string Insert(MaterialConsumptionparam MaterialConsumptionparam)
        {
            // throw new NotImplementedException();
            var outputId = new SqlParameter
            {

                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@MaterialConsumptionId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = MaterialConsumptionparam.MaterialconsumptionInsert.ToDictionary();
            dic.Remove("MaterialConsumptionId");
            var Id = ExecNonQueryProcWithOutSaveChanges("insert_MaterialConsumption_1", dic, outputId);

            _unitofWork.SaveChanges();

            return Id;
        }
    }
}

