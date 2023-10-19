using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
    public class R_GRN : GenericRepository, I_GRN
    {
        public R_GRN(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String InsertGRNDirect(GRNParams grnParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@GRNID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@GRNDetID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = grnParams.GRNSave.ToDictionary();
            disc3.Remove("GRNID");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_GRNHeader_PurNo_1_New", disc3, outputId1);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                disc5.Remove("GRNDetID");
                disc5["GRNId"] = BillNo;
               var GrnDetID= ExecNonQueryProcWithOutSaveChanges("insert_GRNDetails_1_New", disc5,outputId2);
            }
            foreach (var a in grnParams.UpdateItemMasterGSTPer)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_M_ItemMaster_GSTPer_1", disc5);
            }

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public bool UpdateGRN(GRNParams grnParams)
        { 
            var vPurchaseOrderUdpate = grnParams.updateGRNHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_GRNHeader_1", vPurchaseOrderUdpate);

            var vPurchaseOrderDelete = grnParams.Delete_GRNDetails.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Delete_GRNDetails_1", vPurchaseOrderDelete);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_GRNDetails_1_New", disc5);
            }

            _unitofWork.SaveChanges();
            return true;

        }
        public string InsertGRNPurchase(GRNParams grnParams)
        {
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@GRNID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = grnParams.GRNSave.ToDictionary();
            disc3.Remove("GRNID");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_GRNHeader_PurNo_1_New", disc3, outputId1);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                disc5["GRNID"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_GRNDetails_1_New", disc5);
            }
            foreach (var a in grnParams.UpdateItemMasterGSTPer)
            {
                var vItemMasterGST = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("Update_M_ItemMaster_GSTPer_1", vItemMasterGST);
            }
            foreach (var a in grnParams.Update_PO_STATUS_AganistGRN)
            {
                var vPODet = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("Update_PO_STATUS_AganistGRN", vPODet);
            }
            foreach (var a in grnParams.Update_POHeader_Status_AganistGRN)
            {
                var vPOHeader = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("Update_POHeader_Status_AganistGRN", vPOHeader);
            }
            _unitofWork.SaveChanges();
            return BillNo;
        }
        public bool VerifyGRN(GRNParams grnParams)
        {
            var vGRNVerify = grnParams.UpdateGRNVerifyStatus.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_GRN_Verify_Status_1", vGRNVerify);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
