using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Inventory;
using System.IO;

namespace HIMS.Data.Inventory
{
    public class R_StockAdjustment : GenericRepository, I_StockAdjustment
    {
        public R_StockAdjustment(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String StockAdjustment(StockAdjustmentParams stockAdjustmentParams)
        {

            var vStockAdgId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@StockAdgId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = stockAdjustmentParams.StockAdjustment.ToDictionary();
            disc3.Remove("StockAdgId");
            var StockAdgId = ExecNonQueryProcWithOutSaveChanges("m_Phar_stockAjustment_1", disc3, vStockAdgId);

            _unitofWork.SaveChanges();
            return StockAdgId;
        }

        public bool BatchAdjustment(StockAdjustmentParams stockAdjustmentParams)
        {

            var vBatchAdjustment = stockAdjustmentParams.BatchAdjustment.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Phar_BatchExpDate_stockAjustment_1", vBatchAdjustment);

            _unitofWork.SaveChanges();
            return true;
        }

    }
}
