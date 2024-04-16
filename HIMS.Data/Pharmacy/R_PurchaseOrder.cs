using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_PurchaseHeader_WithPurNo_1", disc3, outputId1);

            foreach (var a in purchaseParams.PurchaseDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["PurchaseId"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_PurchaseDetails_1", disc5);
            }

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public bool UpdatePurchaseOrder(PurchaseParams purchaseParams)
        {
            var vPurchaseOrderUdpate = purchaseParams.UpdatePurchaseOrderHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_PurchaseHeader", vPurchaseOrderUdpate);

            var vPurchaseOrderDelete = purchaseParams.Delete_PurchaseDetails.ToDictionary();
            vPurchaseOrderDelete["PurchaseId"] = purchaseParams.UpdatePurchaseOrderHeader.PurchaseID;
            ExecNonQueryProcWithOutSaveChanges("m_Delete_PurchaseDetails_1", vPurchaseOrderDelete);

            foreach (var a in purchaseParams.PurchaseDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["PurchaseId"] = purchaseParams.UpdatePurchaseOrderHeader.PurchaseID;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_PurchaseDetails_1", disc5);
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

        public string ViewPurchaseorder(int PurchaseID, string htmlFilePath, string htmlHeaderFilePath)
        {
            //  throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];
          
            para[0] = new SqlParameter("@PurchaseID", PurchaseID) { DbType = DbType.String };
          
            var Bills = GetDataTableProc("rptPrintPurchaseOrder", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkdiscflag = false;

            double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\" font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;border-bottom:1px solid #000;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0px;height:10px;text-align:center;vertical-align:middle;border-bottom:1px solid #000;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0px;height:10px;vertical-align:middle;text-align: center;border-bottom:1px solid #000;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0px;height:10px;vertical-align:middle;text-align: center;border-bottom:1px solid #000;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0px;height:10px;vertical-align:middle;text-align: center;border-bottom:1px solid #000;\">").Append(dr["MRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align:center;border-bottom:1px solid #000;\">").Append(dr["Rate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["ItemTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["DiscPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["CGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["SGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["IGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["GrandTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalAmount += dr["ItemTotalAmount"].ConvertToDouble();
                T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                T_TotalDiscAmount += dr["ItemDiscAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["GrandTotalAmount"].ConvertToDouble();
                //T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                //T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                //T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                //T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


            }
            //| currency:'INR':'symbol-narrow':'0.2'

            T_TotalNETAmount = Math.Round(T_TotalNETAmount);
            T_TotalVatAmount = Math.Round(T_TotalVatAmount);
            T_TotalDiscAmount = Math.Round(T_TotalDiscAmount);
            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());


            html = html.Replace("{{FreightAmount}}", Bills.GetColValue("FreightAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{OctriAmount}}", Bills.GetColValue("OctriAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalAmount}}", Bills.GetColValue("TotalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{HandlingCharges}}", Bills.GetColValue("HandlingCharges").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{TransportChanges}}", Bills.GetColValue("TransportChanges").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AddedByName}}", Bills.GetColValue("AddedByName"));
            html = html.Replace("{{VatAmount}}", Bills.GetColValue("VatAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Remarks}}", Bills.GetColValue("Remarks"));
            html = html.Replace("{{PurchaseDate}}", Bills.GetColValue("PurchaseDate").ConvertToDateString("dd/mm/yyyy hh:mm tt"));
            html = html.Replace("{{SupplierName}}", Bills.GetColValue("SupplierName").ConvertToString());
            html = html.Replace("{{PurchaseNo}}", Bills.GetColValue("PurchaseNo").ConvertToString());
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{Email}}", Bills.GetColValue("Email").ConvertToString());
            html = html.Replace("{{GSTNo}}", Bills.GetColValue("GSTNo").ConvertToString());
            html = html.Replace("{{Mobile}}", Bills.GetColValue("Mobile"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{VatAmount}}", Bills.GetColValue("VatAmount").ConvertToString());
            html = html.Replace("{{Mobile}}", Bills.GetColValue("Mobile"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            html = html.Replace("{{PurchaseTime}}", Bills.GetColValue("PurchaseTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

            string finalamt = conversion(T_TotalNETAmount.ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");

            return html;
        }

        public string conversion(string amount)
        {
            double m = Convert.ToInt64(Math.Floor(Convert.ToDouble(amount)));
            double l = Convert.ToDouble(amount);

            double j = (l - m) * 100;
            //string Word = " ";

            var beforefloating = ConvertNumbertoWords(Convert.ToInt64(m));
            var afterfloating = ConvertNumbertoWords(Convert.ToInt64(j));

            // Word = beforefloating + '.' + afterfloating;

            var Content = beforefloating + ' ' + " RUPEES" + ' ' + afterfloating + ' ' + " PAISE only";

            return Content;
        }

        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
           {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
           {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

    }
}
