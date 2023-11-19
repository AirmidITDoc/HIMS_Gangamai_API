using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
  public  class R_Workorder:GenericRepository,I_Workorder
    {
        public R_Workorder(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string InsertWorkOrder(Workorder Workorder)
        {
            //  throw new NotImplementedException();
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@WOId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = Workorder.WorkorderHeaderInsert.ToDictionary();
            disc3.Remove("WOId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_T_WorkOrderHeader_1", disc3, outputId1);

            foreach (var a in Workorder.WorkorderDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["WOId"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_T_WorkOrderDetail_1", disc5);
            }

            _unitofWork.SaveChanges();
            return BillNo;

        }

        public bool UpdateWorkOrder(Workorder Workorder)
        {
            //throw new NotImplementedException();

            var vPurchaseOrderUdpate = Workorder.UpdateWorkOrderHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_WorkorderHeader", vPurchaseOrderUdpate);

            var vPurchaseOrderDelete = Workorder.Delete_WorkDetails.ToDictionary();
            vPurchaseOrderDelete["WOId"] = Workorder.UpdateWorkOrderHeader.WOId;
            ExecNonQueryProcWithOutSaveChanges("Delete_WODetails_1", vPurchaseOrderDelete);

            foreach (var a in Workorder.WorkorderDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["WOId"] = Workorder.UpdateWorkOrderHeader.WOId;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_T_WorkOrderDetail_1", disc5);
            }

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
