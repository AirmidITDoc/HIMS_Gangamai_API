using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPDischarge:GenericRepository,I_IPDischarge
    {
        public readonly SqlCommand command;

        public R_IPDischarge(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

         public String Insert(IPDischargeParams IPDischargeParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DischargeId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = IPDischargeParams.InsertIPDDischarg.ToDictionary();
            dic.Remove("DischargeId");
            var DischargeId = ExecNonQueryProcWithOutSaveChanges("m_insert_Discharge_1", dic, outputId);

            IPDischargeParams.UpdateAdmission.AdmissionID = Convert.ToInt32(IPDischargeParams.InsertIPDDischarg.AdmissionId);
            var disc2 = IPDischargeParams.UpdateAdmission.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_Admission_3", disc2);

            var vDischargeBedRelease = IPDischargeParams.DischargeBedRelease.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_DischargeBedRelease", vDischargeBedRelease);

            _unitofWork.SaveChanges();
            return DischargeId;

        }

        public bool Update(IPDischargeParams IPDischargeParams)
        {
            // new NotImplementedException();

            var disc4 = IPDischargeParams.UpdateIPDDischarg.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_Discharge_1", disc4);

            //IPDischargeParams.UpdateAdmission.AdmissionID = Convert.ToInt32(IPDischargeParams.UpdateIPDDischarg.AdmissionId);
            var disc2 = IPDischargeParams.UpdateAdmission.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Admission_3", disc2);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewDischargeReceipt(int AdmId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmId", AdmId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptDischargeCheckOutSlip", para);
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");

            
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceNo}}", Bills.GetColValue("AdvanceNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));

            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));

            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DischargeTime}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillTime}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));




            html = html.Replace("{{BillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{DischargeTime}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

            html = html.Replace("{{OPD_IPD_ID}}", Bills.GetColValue("OPD_IPD_ID"));
            html = html.Replace("{{BillingUserName}}", Bills.GetColValue("BillingUserName"));
                          

            return html;
        }
    }
}

