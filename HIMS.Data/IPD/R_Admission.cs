﻿using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_Admission : GenericRepository, I_Admission
    {
        public R_Admission(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public string AdmissionListCurrent(int DoctorId, int WardId, int CompanyId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@CompanyId", CompanyId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCurrentAdmittedListReport", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_Count = 0;
            double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DOT"].ConvertToDateString("dd/MM/yy")).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");



                T_Count = T_Count + 1;
            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());

            return html;


        }
        public DataTable GetDataForReport(int AdmissionId)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@AdmissionId", AdmissionId) { DbType = DbType.Int64 };
            return GetDataTableProc("m_rptAdmissionPrint", para);
        }
        public string ViewAdmissionPaper(int AdmissionId, string htmlFilePath, string htmlHeader)
        {
            //  throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@AdmissionId", AdmissionId) { DbType = DbType.Int64 };

            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            var Bills = GetDataTableProc("m_rptAdmissionPrint", para);

            html = html.Replace("{{DataContent}}", htmlHeader);


            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("MobileNo"));
            html = html.Replace("{{PhoneNo}}", Bills.GetColValue("PhoneNo"));

            html = html.Replace("{{DOT}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yy hh:mm tt"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));

            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));


            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RefDoctorName}}", Bills.GetColValue("RefDoctorName"));

            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            html = html.Replace("{{RelativeName}}", Bills.GetColValue("RelativeName"));
            html = html.Replace("{{RelativePhoneNo}}", Bills.GetColValue("RelativePhoneNo"));

            html = html.Replace("{{RelationshipName}}", Bills.GetColValue("RelationshipName"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{IsMLC}}", Bills.GetColValue("IsMLC"));
            html = html.Replace("{{AdmittedDoctor1}}", Bills.GetColValue("AdmittedDoctor1"));
            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));

            html = html.Replace("{{MaritalStatusName}}", Bills.GetColValue("MaritalStatusName"));
            html = html.Replace("{{AadharcardNo}}", Bills.GetColValue("AadharcardNo"));
            html = html.Replace("{{TariffName}}", Bills.GetColValue("TariffName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));

            html = html.Replace("{{AdmittedDoctor1}}", Bills.GetColValue("AdmittedDoctor1"));

            html = html.Replace("{{chkMLCflag}}", Bills.GetColValue("IsMLC").ToBool() == true ? "table-row " : "none");
            html = html.Replace("{{chkMLCflag1}}", Bills.GetColValue("IsMLC").ToBool() == false ? "table-row " : "none");

            html = html.Replace("{{DOA}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

            html = html.Replace("{{AdmittedDoctor2}}", Bills.GetColValue("AdmittedDoctor2"));
            html = html.Replace("{{LoginUserSurname}}", Bills.GetColValue("LoginUserSurname"));

            return html;

        }




        public bool AdmissionUpdate(AdmissionParams AdmissionParams)
        {
            // throw new NotImplementedException();

            var disc1 = AdmissionParams.AdmissionNewUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Admission_1", disc1);


            _unitofWork.SaveChanges();
            return true;


        }

        public String AdmissionNewInsert(AdmissionParams AdmissionParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdmissionID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc2 = AdmissionParams.RegInsert.ToDictionary();
            disc2.Remove("RegID");
            var RegId = ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", disc2, outputId1);

            AdmissionParams.AdmissionNewInsert.RegId = Convert.ToInt32(RegId);
            var disc1 = AdmissionParams.AdmissionNewInsert.ToDictionary();
            disc1.Remove("AdmissionID");
            var AdmissionID = ExecNonQueryProcWithOutSaveChanges("insert_Admission_1", disc1, outputId);

            //BedUpdate
            var BedId = AdmissionParams.BedStatusUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_AdmissionBedstatus", BedId);

            //IpSMSTemplate
            // var disc3 = AdmissionParams.IpSMSTemplateInsert.ToDictionary();
            //  disc2.Remove("RegId");
            //ExecNonQueryProcWithOutSaveChanges("Insert_IPSMSTemplete_1", disc3);

            //foreach (var a in itemMasterParams.InsertAssignItemToStore)
            //{
            //    var disc = a.ToDictionary();
            //    disc["ItemId"] = itemId;
            //    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_assignItemToStore", disc);
            //}



            _unitofWork.SaveChanges();
            return (AdmissionID);
        }

        public string AdmissionRegistredInsert(AdmissionParams AdmissionParams)
        {
            // throw new NotImplementedException();

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdmissionID",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc1 = AdmissionParams.AdmissionNewInsert.ToDictionary();
            disc1.Remove("AdmissionID");
            var AdmissionID = ExecNonQueryProcWithOutSaveChanges("insert_Admission_1", disc1, outputId);
            //BedUpdate
            var BedId = AdmissionParams.BedStatusUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_AdmissionBedstatus", BedId);


            _unitofWork.SaveChanges();
            return AdmissionID;


        }

        public string AdmissionListCurrentHospitaldetail(int DoctorId, int WardId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.String };
            para[1] = new SqlParameter("@WardId", WardId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptCurrentAdmittedList", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";

            double T_TotalBillAmount = 0, T_TotalAdvAmount = 0, T_TotalBalAmount = 0;
            String Label = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1)
                {

                    Label = dr["RoomName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["RoomName"].ConvertToString();

                if (Label == previousLabel)
                {
                    j = 1;

                    items.Append("<tr><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdmissionTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");


                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesAmount"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdvanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesAmount"].ConvertToString()).Append("</td></tr>");




                    T_TotalBillAmount += dr["ChargesAmount"].ConvertToDouble();
                    T_TotalAdvAmount += dr["AdvanceAmount"].ConvertToDouble();
                    T_TotalBalAmount += dr["ApprovedAmount"].ConvertToDouble();
                    j++;
                }
            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalAdvAmount}}", T_TotalAdvAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalBalAmount}}", T_TotalBalAmount.To2DecimalPlace());
            //html = html.Replace("{{T_TotalBalafterAdvAmount}}", T_TotalBalafterAdvAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());
            //html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            //html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());

            return html;
        }

        public string AdmissionListCurrentPharmacydetail(int DoctorId, int WardId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.String };
            para[1] = new SqlParameter("@WardId", WardId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptCurrentAdmittedList", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";

            double T_TotalBillAmount = 0, T_TotalCreditAmount = 0, T_TotalAdvbalAmount = 0, T_TotalBalafterAdvAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;
            String Label = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1)
                {

                    Label = dr["RoomName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["RoomName"].ConvertToString();

                if (Label == previousLabel)
                {
                    j = 1;

                    items.Append("<tr><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdmissionTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");


                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesAmount"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesAmount"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdvanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ApprovedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                    T_TotalBillAmount += dr["ChargesAmount"].ConvertToDouble();
                    T_TotalCreditAmount += dr["ChargesAmount"].ConvertToDouble();
                    T_TotalAdvbalAmount += dr["AdvanceAmount"].ConvertToDouble();
                    T_TotalBalafterAdvAmount += dr["ApprovedAmount"].ConvertToDouble();
                    j++;
                }
            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalCreditAmount}}", T_TotalCreditAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalAdvbalAmount}}", T_TotalAdvbalAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalBalafterAdvAmount}}", T_TotalBalafterAdvAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());
            //html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            //html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());

            return html;
        }

        public string ViewAdmissiontemplatePaper(DataTable Bills, string htmlFilePath, string htmlHeade)
        {


            //string html = File.ReadAllText(htmlFilePath);
            string html = htmlFilePath;

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            StringBuilder items = new StringBuilder("");
            int i = 0;

            html = html.Replace("{{DataContent}}", htmlHeade);


            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("MobileNo"));
            html = html.Replace("{{PhoneNo}}", Bills.GetColValue("PhoneNo"));

            html = html.Replace("{{DOT}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yy hh:mm tt"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));

            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));


            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RefDoctorName}}", Bills.GetColValue("RefDoctorName"));

            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            html = html.Replace("{{RelativeName}}", Bills.GetColValue("RelativeName"));
            html = html.Replace("{{RelativePhoneNo}}", Bills.GetColValue("RelativePhoneNo"));

            html = html.Replace("{{RelationshipName}}", Bills.GetColValue("RelationshipName"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{IsMLC}}", Bills.GetColValue("IsMLC"));
            html = html.Replace("{{AdmittedDoctor1}}", Bills.GetColValue("AdmittedDoctor1"));
            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));

            html = html.Replace("{{MaritalStatusName}}", Bills.GetColValue("MaritalStatusName"));
            html = html.Replace("{{AadharcardNo}}", Bills.GetColValue("AadharcardNo"));
            html = html.Replace("{{TariffName}}", Bills.GetColValue("TariffName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));

            html = html.Replace("{{AdmittedDoctor1}}", Bills.GetColValue("AdmittedDoctor1"));

            html = html.Replace("{{chkMLCflag}}", Bills.GetColValue("IsMLC").ToBool() == true ? "table-row " : "none");
            html = html.Replace("{{chkMLCflag1}}", Bills.GetColValue("IsMLC").ToBool() == false ? "table-row " : "none");

            html = html.Replace("{{DOA}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

            html = html.Replace("{{AdmittedDoctor2}}", Bills.GetColValue("AdmittedDoctor2"));
            html = html.Replace("{{LoginUserSurname}}", Bills.GetColValue("LoginUserSurname"));



            html = html.Replace("{{chkyearflag}}", Bills.GetColValue("AgeYear").ToInt() == 0 ? "none" : "visible");
            html = html.Replace("{{chkmonthflag}}", Bills.GetColValue("AgeMonth").ToInt() == 0 ? "none" : "visible");
            html = html.Replace("{{chkdayflag}}", Bills.GetColValue("AgeDay").ToInt() == 0 ? "none" : "visible");

            return html;
        }



    }
}
