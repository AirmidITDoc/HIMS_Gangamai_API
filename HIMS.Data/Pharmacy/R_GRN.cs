﻿using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Pharmacy
{
    public class R_GRN : GenericRepository, I_GRN
    {
        public R_GRN(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String InsertGRNDirectNEW(GRNParamsNEW grnParams)
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

            foreach (var a in grnParams.InsertTGRNRetDet)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_GRN_RetDet_1", disc);
            }


            var disc3 = grnParams.GRNSave.ToDictionary();
            disc3.Remove("GRNID");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNHeader_PurNo_1_New", disc3, outputId1);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                disc5.Remove("GRNDetID");
                disc5["GRNId"] = BillNo;
                var GrnDetID = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNDetails_1_New ", disc5, outputId2);
            }
            foreach (var a in grnParams.UpdateItemMasterGSTPer)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Update_M_ItemMaster_GSTPer_1", disc5);
            }

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public bool UpdateGRNNEW(GRNParamsNEW grnParams)
        {


            var vDelete = grnParams.DeleteRetDet.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Delete_T_GRN_RetDet", vDelete);



            foreach (var a in grnParams.InsertTGRNRetDet)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_GRN_RetDet_1", disc);
            }

            var vPurchaseOrderUdpate = grnParams.updateGRNHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_GRNHeader_1", vPurchaseOrderUdpate);

            var vPurchaseOrderDelete = grnParams.Delete_GRNDetails.ToDictionary();
            vPurchaseOrderDelete["GRNId"] = grnParams.updateGRNHeader.GRNID;
            ExecNonQueryProcWithOutSaveChanges("m_Delete_GRNDetails_1_1", vPurchaseOrderDelete);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNDetails_1_New", disc5);
            }

            _unitofWork.SaveChanges();
            return true;

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
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNHeader_PurNo_1_New", disc3, outputId1);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                disc5.Remove("GRNDetID");
                disc5["GRNId"] = BillNo;
                var GrnDetID = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNDetails_1_New ", disc5, outputId2);
            }
            foreach (var a in grnParams.UpdateItemMasterGSTPer)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Update_M_ItemMaster_GSTPer_1", disc5);
            }

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public bool UpdateGRN(GRNParams grnParams)
        {
            var vPurchaseOrderUdpate = grnParams.updateGRNHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_GRNHeader_1", vPurchaseOrderUdpate);

            var vPurchaseOrderDelete = grnParams.Delete_GRNDetails.ToDictionary();
            vPurchaseOrderDelete["GRNId"] = grnParams.updateGRNHeader.GRNID;
            ExecNonQueryProcWithOutSaveChanges("m_Delete_GRNDetails_1_1", vPurchaseOrderDelete);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNDetails_1_New", disc5);
            }

            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateGRNPurchase(GRNParams grnParams)
        {

            var vPurchaseOrderUdpate = grnParams.updateGRNHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_GRNHeader_1", vPurchaseOrderUdpate);

            var vPurchaseOrderDelete = grnParams.Delete_GRNDetails.ToDictionary();
            vPurchaseOrderDelete["GRNId"] = grnParams.updateGRNHeader.GRNID;
            ExecNonQueryProcWithOutSaveChanges("m_Delete_GRNDetails_1_1", vPurchaseOrderDelete);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNDetails_1_New", disc5);
            }
            foreach (var a in grnParams.UpdateItemMasterGSTPer)
            {
                var vItemMasterGST = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_Update_M_ItemMaster_GSTPer_1", vItemMasterGST);
            }
            foreach (var a in grnParams.Update_PO_STATUS_AganistGRN)
            {
                var vPODet = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_Update_PO_STATUS_AganistGRN", vPODet);
            }
            foreach (var a in grnParams.Update_POHeader_Status_AganistGRN)
            {
                var vPOHeader = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_Update_POHeader_Status_AganistGRN", vPOHeader);
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

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@GRNDetID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = grnParams.GRNSave.ToDictionary();
            disc3.Remove("GRNID");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNHeader_PurNo_1_New", disc3, outputId1);

            foreach (var a in grnParams.GRNDetailSave)
            {
                var disc5 = a.ToDictionary();
                disc5.Remove("GRNDetID");
                disc5["GRNId"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNDetails_1_New", disc5, outputId2);
            }
            foreach (var a in grnParams.UpdateItemMasterGSTPer)
            {
                var vItemMasterGST = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_Update_M_ItemMaster_GSTPer_1", vItemMasterGST);
            }
            foreach (var a in grnParams.Update_PO_STATUS_AganistGRN)
            {
                var vPODet = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_Update_PO_STATUS_AganistGRN", vPODet);
            }
            foreach (var a in grnParams.Update_POHeader_Status_AganistGRN)
            {
                var vPOHeader = a.ToDictionary();
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_Update_POHeader_Status_AganistGRN", vPOHeader);
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

        public string ViewGRNReport(int GRNID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@GRNID", GRNID) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptPrintGRN", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkdiscflag = false;
            Boolean chkponoflag = false;

            double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr  style=\"font-size:16px;border: 1px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color: #101828 \"><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;text-align: left; padding: 6px;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center;padding: 6px;\">").Append(dr["ReceiveQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center;padding: 6px;\">").Append(dr["FreeQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right;padding: 6px;\">").Append(dr["MRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;text-align: right; padding: 6px;\">").Append(dr["Rate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;  text-align: center;padding: 6px;\">").Append(dr["SGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ItemWiseGST"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_TotalVatAmount += dr["TotalVATAmount"].ConvertToDouble();
                T_TotalDiscAmount += dr["TotalDiscAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["NetPayble"].ConvertToDouble();
                //T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


            }
            //| currency:'INR':'symbol-narrow':'0.2'
            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", Bills.GetColValue("TotalDiscAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());


            html = html.Replace("{{TotCGSTAmt}}", Bills.GetColValue("TotCGSTAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{GrandTotalAount}}", Bills.GetColValue("GrandTotalAount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalAmount}}", Bills.GetColValue("TotalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{HandlingCharges}}", Bills.GetColValue("HandlingCharges").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{TransportChanges}}", Bills.GetColValue("TransportChanges").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotSGSTAmt}}", Bills.GetColValue("TotSGSTAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", Bills.GetColValue("TotalDiscAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{OtherCharge}}", Bills.GetColValue("OtherCharge").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CreditNote}}", Bills.GetColValue("CreditNote").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AddedByName}}", Bills.GetColValue("AddedByName").ConvertToString());
            html = html.Replace("{{DebitNote}}", Bills.GetColValue("DebitNote").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark").ConvertToString());
            html = html.Replace("{{TotalVATAmount}}", Bills.GetColValue("TotalVATAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NetPayble}}", Bills.GetColValue("NetPayble").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{T_TotalCGST}}",T_TotalCGST.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_TotalSGST}}", T_TotalSGST.ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{GRNDate}}", Bills.GetColValue("GRNDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{GRNTime}}", Bills.GetColValue("GRNDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PurchaseTime}}", Bills.GetColValue("PurchaseTime").ConvertToDateString("dd/MM/yyyy"));
            
            html = html.Replace("{{InvDate}}", Bills.GetColValue("InvDate").ConvertToDateString("dd/MM/yyyy"));


            html = html.Replace("{{GateEntryNo}}", Bills.GetColValue("GateEntryNo"));
            html = html.Replace("{{GrnNumber}}", Bills.GetColValue("GrnNumber"));
            html = html.Replace("{{EwayBillDate}}", Bills.GetColValue("EwayBillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{GSTNo}}", Bills.GetColValue("GSTNo").ConvertToString());
            html = html.Replace("{{EwayBillNo}}", Bills.GetColValue("EwayBillNo"));


            html = html.Replace("{{SupplierName}}", Bills.GetColValue("SupplierName").ConvertToString());
            html = html.Replace("{{PONo}}", Bills.GetColValue("PONo").ConvertToString());
            html = html.Replace("{{InvoiceNo}}", Bills.GetColValue("InvoiceNo").ConvertToString());
            
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{Email}}", Bills.GetColValue("Email").ConvertToString());
            html = html.Replace("{{GateEnteryNo}}", Bills.GetColValue("GateEnteryNo").ConvertToString());
            //html = html.Replace("{{Mobile}}", Bills.GetColValue("Mobile"));
            //html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{VatAmount}}", Bills.GetColValue("VatAmount").ConvertToString());
            html = html.Replace("{{Mobile}}", Bills.GetColValue("Mobile"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PONo}}", Bills.GetColValue("PONo"));

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            string finalamt = NumberToWords(Bills.GetColValue("NetPayble").ConvertToDouble().To2DecimalPlace().ToInt());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");


            html = html.Replace("{{chkponoflag}}", Bills.GetColValue("PONo").ConvertToDouble() != 0 ? "table-row " : "none");


            return html;
        }


        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}