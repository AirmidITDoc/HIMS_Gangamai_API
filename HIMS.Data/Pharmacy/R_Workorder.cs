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
            ExecNonQueryProcWithOutSaveChanges("Delete_WODetails_1", vPurchaseOrderDelete);

            foreach (var a in Workorder.WorkorderDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["WOId"] = Workorder.UpdateWorkOrderHeader.WOId;
                ExecNonQueryProcWithOutSaveChanges("insert_T_WorkOrderDetail_1", disc5);
            }

            //var vPurchaseOrderDelete = Workorder.Delete_WorkDetails.ToDictionary();
            //vPurchaseOrderDelete["WOId"] = Workorder.UpdateWorkOrderHeader.WOId;
            //ExecNonQueryProcWithOutSaveChanges("Delete_WODetails_1", vPurchaseOrderDelete);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewPurWorkorder(int WOID, string htmlFilePath, string htmlHeaderFilePath)
        {
            //  throw new NotImplementedException();
            

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@WOID", WOID) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptWorkOrderPrint", para);
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
                items.Append("<td style=\"border-left:1px solid #000;padding:0px;height:10px;vertical-align:middle;text-align: center;border-bottom:1px solid #000;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align:center;border-bottom:1px solid #000;\">").Append(dr["Rate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["DiscPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["VATPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["WoNetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_TotalVatAmount += dr["VATAmount"].ConvertToDouble();
                T_TotalDiscAmount += dr["DiscAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["WoNetAmount"].ConvertToDouble();
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


            //html = html.Replace("{{FreightAmount}}", Bills.GetColValue("FreightAmount").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{OctriAmount}}", Bills.GetColValue("OctriAmount").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{TotalAmount}}", Bills.GetColValue("TotalAmount").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{HandlingCharges}}", Bills.GetColValue("HandlingCharges").ConvertToDouble().To2DecimalPlace());

            //html = html.Replace("{{TransportChanges}}", Bills.GetColValue("TransportChanges").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{AddedByName}}", Bills.GetColValue("AddedByName"));
            //html = html.Replace("{{VatAmount}}", Bills.GetColValue("VatAmount").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount").ConvertToDouble().To2DecimalPlace());
         
            //html = html.Replace("{{PurchaseDate}}", Bills.GetColValue("PurchaseDate").ConvertToDateString("dd/mm/yyyy hh:mm tt"));
       
           
          
            //html = html.Replace("{{Email}}", Bills.GetColValue("Email").ConvertToString());
            //html = html.Replace("{{GSTNo}}", Bills.GetColValue("GSTNo").ConvertToString());
       
            //html = html.Replace("{{VatAmount}}", Bills.GetColValue("VatAmount").ConvertToString());
            //html = html.Replace("{{Mobile}}", Bills.GetColValue("Mobile"));
            //html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            //html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            //html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
           
            
            html = html.Replace("{{AddedbyName}}", Bills.GetColValue("AddedbyName"));
            html = html.Replace("{{SupplierName}}", Bills.GetColValue("SupplierName").ConvertToString());
            html = html.Replace("{{WOId}}", Bills.GetColValue("WOId").ConvertToString());
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{Date}}", Bills.GetColValue("Time").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{Mobile}}", Bills.GetColValue("Mobile"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{Remarks}}", Bills.GetColValue("WORemark"));

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
