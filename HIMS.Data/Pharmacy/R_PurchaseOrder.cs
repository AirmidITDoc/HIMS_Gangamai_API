using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
    public class R_PurchaseOrder : GenericRepository, I_PurchaseOrder
    {
        public R_PurchaseOrder(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String InsertPurchaseOrder(PurchaseParams purchaseParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PurchaseId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = purchaseParams.PurchaseHeaderInsert.ToDictionary();
            disc3.Remove("PurchaseId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_PurchaseHeader_WithPurNo_1", disc3, outputId1);

            foreach (var a in purchaseParams.PurchaseDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["PurchaseId"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_PurchaseDetails_1", disc5);
            }

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public bool UpdatePurchaseOrder(PurchaseParams purchaseParams)
        {
            var vPurchaseOrderUdpate = purchaseParams.UpdatePurchaseOrderHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_PurchaseHeader", vPurchaseOrderUdpate);

            var vPurchaseOrderDelete = purchaseParams.Delete_PurchaseDetails.ToDictionary();
            vPurchaseOrderDelete["PurchaseId"] = purchaseParams.UpdatePurchaseOrderHeader.PurchaseID;
            ExecNonQueryProcWithOutSaveChanges("Delete_PurchaseDetails_1", vPurchaseOrderDelete);

            foreach (var a in purchaseParams.PurchaseDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["PurchaseId"] = purchaseParams.UpdatePurchaseOrderHeader.PurchaseID;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_PurchaseDetails_1", disc5);
            }

            _unitofWork.SaveChanges();
            return true;
        }
        public bool VerifyPurchaseOrder(PurchaseParams purchaseParams)
        {
            var vDiscCal = purchaseParams.Update_POVerify_Status.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_POVerify_Status_1", vDiscCal);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
