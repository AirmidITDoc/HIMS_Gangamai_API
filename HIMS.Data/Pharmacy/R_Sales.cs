using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
    public class R_Sales : GenericRepository, I_Sales
    {
        public R_Sales(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String InsertSales(SalesParams salesParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SalesId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = salesParams.SalesInsert.ToDictionary();
            disc3.Remove("SalesId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Sales_1", disc3, outputId1);

            foreach (var a in salesParams.SalesDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["SalesID"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_SalesDetails_1", disc5);
            }

            foreach (var a in salesParams.UpdateCurStkSales)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_T_CurStk_Sales_Id_1", disc1);
            }

            var vDiscCal = salesParams.Cal_DiscAmount_Sales.ToDictionary();
            vDiscCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_Sales", vDiscCal);

            var vGSTCal = salesParams.Cal_GSTAmount_Sales.ToDictionary();
            vGSTCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("m_Cal_GSTAmount_Sales", vDiscCal);

            var vPayment = salesParams.SalesPayment.ToDictionary();
            vPayment["BillNo"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_Pharmacy_New_1", vPayment);

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public String InsertSalesWithCredit(SalesCreditParams salesCreditParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SalesId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = salesCreditParams.SalesInsertCredit.ToDictionary();
            disc3.Remove("SalesId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Sales_1", disc3, outputId1);

            foreach (var a in salesCreditParams.SalesDetailInsertCredit)
            {
                var disc5 = a.ToDictionary();
                disc5["SalesID"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_SalesDetails_1", disc5);
            }

            foreach (var a in salesCreditParams.UpdateCurStkSalesCredit)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_T_CurStk_Sales_Id_1", disc1);
            }

            var vDiscCal = salesCreditParams.Cal_DiscAmount_SalesCredit.ToDictionary();
            vDiscCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_Sales", vDiscCal);

            var vGSTCal = salesCreditParams.Cal_GSTAmount_SalesCredit.ToDictionary();
            vGSTCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("m_Cal_GSTAmount_Sales", vDiscCal);

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public bool PaymentSettlement(SalesParams salesParams)
        {
            var vPayment = salesParams.SalesPayment.ToDictionary();
            vPayment.Remove("PaymentID");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_Pharmacy_New_1", vPayment);

            var vUpdateHeader = salesParams.update_Pharmacy_BillBalAmount.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Pharmacy_BillBalAmount_1", vUpdateHeader);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
