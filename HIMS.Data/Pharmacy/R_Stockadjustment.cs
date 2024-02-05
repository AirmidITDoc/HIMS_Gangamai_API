using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_Stockadjustment:GenericRepository,I_Stokadjustment
    {
        public R_Stockadjustment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public string Insert(Stockadjustmentparam Stockadjustmentparam)
        {
            // throw new NotImplementedException
            // 
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@StockAdgId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc1 = Stockadjustmentparam.InsertMRPStockadju.ToDictionary();
            disc1.Remove("StockAdgId");
            var Id = ExecNonQueryProcWithOutSaveChanges("insert_T_MRPStockAdjestment_1", disc1, outputId);


            Stockadjustmentparam.Updatecurrentstockadjyadd.StoreId = Convert.ToInt32(Stockadjustmentparam.InsertMRPStockadju.StoreID);
            Stockadjustmentparam.Updatecurrentstockadjyadd.ItemId = Convert.ToInt32(Stockadjustmentparam.InsertMRPStockadju.ItemId);

            var disc2 = Stockadjustmentparam.Updatecurrentstockadjyadd.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_CurrentStock_MRPAdjustment", disc2);

            Stockadjustmentparam.Updatecurrentstockadjydedu.StoreId = Convert.ToInt32(Stockadjustmentparam.InsertMRPStockadju.StoreID);
            Stockadjustmentparam.Updatecurrentstockadjydedu.ItemId = Convert.ToInt32(Stockadjustmentparam.InsertMRPStockadju.ItemId);

            var disc3 = Stockadjustmentparam.Updatecurrentstockadjydedu.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_CurrentStock_MRPAdjustment_Deduction", disc3);

            var disc4 = Stockadjustmentparam.Insertitemmovstockadd.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_ItemMovementReport_StockAgj_add_Cursor", disc4);


            var disc5 = Stockadjustmentparam.Insertitemmovstockdedu.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_ItemMovementReport_StockAgj_ded_Cursor", disc5);



            _unitofWork.SaveChanges();
            return (Id);
        }

        public bool Update(Stockadjustmentparam Stockadjustmentparam)
        {
            throw new NotImplementedException();
        }
    }
}
