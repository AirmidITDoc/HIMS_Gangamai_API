using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_Mrpadjustment:GenericRepository,I_Mrpadjustment
    {
        public R_Mrpadjustment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Insert(MRPAdjustment MRPAdjustment)
        {
            // throw new NotImplementedException();
            var disc1 = MRPAdjustment.InsertMRPAdju.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_T_MrpAdjustment_1", disc1);

            var disc2 = MRPAdjustment.InsertMRPAdjuNew.ToDictionary();
            disc2["StoreId"] = Convert.ToInt32(MRPAdjustment.InsertMRPAdju.StoreId);
            disc2["ItemId"] = Convert.ToInt32(MRPAdjustment.InsertMRPAdju.ItemId);
            disc2["BatchNo"] = MRPAdjustment.InsertMRPAdju.BatchNo;

            //disc1.Remove("AdmissionID");
            var wID1 = ExecNonQueryProcWithOutSaveChanges("Update_Item_MRPAdjustment_New", disc2);


            _unitofWork.SaveChanges();
            return (true);
        }

        public bool update(MRPAdjustment MRPAdjustment)
        {
            // throw new NotImplementedException();

            var disc1 = MRPAdjustment.InsertMRPAdjuNew.ToDictionary();
            //disc1.Remove("AdmissionID");
            var wID1 = ExecNonQueryProcWithOutSaveChanges("Update_Item_MRPAdjustment_New", disc1);
            _unitofWork.SaveChanges();
            return (true);
        }
    }
}
