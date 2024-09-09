using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_SupplierPayment :GenericRepository,I_SupplierPayment
    {
        public R_SupplierPayment(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string InsertGrnsuppay(SupplierPayment SupplierPayment)
        {
            //  throw new NotImplementedException();

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SupPayId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = SupplierPayment.TGRNsupppayInsert.ToDictionary();
            disc3.Remove("SupPayId");
            var SupPayId = ExecNonQueryProcWithOutSaveChanges("insert_T_GRNSupPayment_1", disc3, outputId3);

            foreach (var a1 in SupplierPayment.TGRNHeaderPayStatus)
            {
                var disc5 = a1.ToDictionary();
               // disc5["GrnId"] = disc3["GrnId"];
                var ID = ExecNonQueryProcWithOutSaveChanges("Update_TGRNHeader_PayStatus", disc5);
            }
            
            foreach (var a in SupplierPayment.TSupPayDetPayStatus)
            {
                var disc1 = a.ToDictionary();
              //  disc5["GrnId"] = BillNo;
                var ID1 = ExecNonQueryProcWithOutSaveChanges("insert_T_SupPayDet_PayStatus", disc1);
            }

           
            _unitofWork.SaveChanges();
            return SupPayId;
        }

    
    }
}
