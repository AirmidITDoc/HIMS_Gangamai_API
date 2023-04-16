using HIMS.Common.Utility;
using HIMS.Model.Master;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic; 
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_SupplierMaster:GenericRepository,I_SupplierMaster
    {
        public R_SupplierMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        //Supplier Insert update to store   
        public bool Update(SupplierMasterParams supplierMasterParams)
        {
            var disc1 = supplierMasterParams.UpdateSupplierMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_SupplierMaster_1", disc1);

            var D_Det = supplierMasterParams.DeleteAssignSupplierToStore.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Delete_AssignSuppliertoStore", D_Det);

            foreach (var a in supplierMasterParams.InsertAssignSupplierToStore)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_AssignSupplierToStore_1", disc);
            }

            _unitofWork.SaveChanges();
            return true;
        }

        public bool Insert(SupplierMasterParams supplierMasterParams)
        {
            
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SupplierId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc1 = supplierMasterParams.InsertSupplierMaster.ToDictionary();
            disc1.Remove("SupplierId");
            var supplierId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_SupplierMaster_1_New", disc1, outputId);

            //add SupplierDetails

            foreach (var a in supplierMasterParams.InsertAssignSupplierToStore)
            {
                var disc = a.ToDictionary();
                disc["SupplierId"] = supplierId;
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_AssignSupplierToStore_1", disc);
            }

            _unitofWork.SaveChanges();
            return true;
        }

     /*   public bool Insert(SupplierMasterParams supplierMasterParams)
        {
            throw new NotImplementedException();
        }

        public bool Update(SupplierMasterParams supplierMasterParams)
        {
            throw new NotImplementedException();
        }*/
    }
}
