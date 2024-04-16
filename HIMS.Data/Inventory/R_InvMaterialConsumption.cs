using HIMS.Common.Utility;
using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Inventory
{
   public class R_InvMaterialConsumption:GenericRepository,I_InvMaterialConsumption
    {
        public R_InvMaterialConsumption(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string Insert(MaterialConsumptionParam MaterialConsumptionParam)
        {

            var MaterialConsumptionId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@MaterialConsumptionId ",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = MaterialConsumptionParam.InsertMaterialConsumption.ToDictionary();
            disc3.Remove("MaterialConsumptionId");
            var Id = ExecNonQueryProcWithOutSaveChanges("insert_MaterialConsumption_1", disc3, MaterialConsumptionId);

            foreach (var a in MaterialConsumptionParam.InsertMaterialConsDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["MaterialConsumptionId"] = Id;
                ExecNonQueryProcWithOutSaveChanges("insert_IMaterialConsumptionDetails", disc5);

            }
            foreach (var a in MaterialConsumptionParam.UpdateCurrentStock)
            {
                var disc2 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Upd_T_Curstk_MatC_1", disc2);
            }

            _unitofWork.SaveChanges();
            return Id;
        }
    }
}
