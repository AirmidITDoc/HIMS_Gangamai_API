using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPDDischargeSummary:GenericRepository,I_IPDDischargeSummary
    {

        public R_IPDDischargeSummary(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }
    
        public String Insert(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DischargesummaryId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc = IPDDischargeSummaryParams.InsertIPDDischargSummary.ToDictionary();
            disc.Remove("DischargesummaryId");

            var DischargesummaryId =ExecNonQueryProcWithOutSaveChanges("insert_DischargeSummary_1", disc, outputId);
                       
            //var disc3 = IPDDischargeSummaryParams.IPSMSInsert.ToDictionary();
            //ExecNonQueryProcWithOutSaveChanges("update_DischargeSummary_1", disc3);


           /*  var disc4 = IPDischargeParams.InsertIPSMSTemplete.ToDictionary();
             ExecNonQueryProcWithOutSaveChanges("update_Admission_2", disc4);


            IPDDischargeSummaryParams.UpdateAdmisionDischargeSummary.AdmissionId = Convert.ToInt32(IPDDischargeSummaryParams.InsertIPDDischargSummary.AdmissionId);
            var disc2 = IPDDischargeSummaryParams.UpdateAdmisionDischargeSummary.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Admission_2", disc2);*/


            _unitofWork.SaveChanges();
            return DischargesummaryId;
        }

        public bool Update(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {
          //  throw new NotImplementedException();

            var disc3 = IPDDischargeSummaryParams.UpdateIPDDischargSummary.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_DischargeSummary_1", disc3);


            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewDischargeSummary(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            //  throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptDischargeSummaryPrint_New", para);

            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{DischargeTime}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{Followupdate}}", Bills.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            
            html = html.Replace("{{History}}", Bills.GetColValue("History"));
            html = html.Replace("{{Diagnosis}}", Bills.GetColValue("Diagnosis"));
            html = html.Replace("{{ClinicalFinding}}", Bills.GetColValue("ClinicalFinding"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{OpertiveNotes}}", Bills.GetColValue("OpertiveNotes"));
            html = html.Replace("{{TreatmentGiven}}", Bills.GetColValue("TreatmentGiven"));
            html = html.Replace("{{Investigation}}", Bills.GetColValue("Investigation"));

            //html = html.Replace("{{OpertiveNotes}}", Bills.GetColValue("OpertiveNotes"));
            //html = html.Replace("{{TreatmentGiven}}", Bills.GetColValue("TreatmentGiven"));
            //html = html.Replace("{{Investigation}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{chkHistoryflag}}", Bills.GetColValue("History").ConvertToDouble() != ' ' ? "table-row " : "none");

            html = html.Replace("{{chkDignosflag}}", Bills.GetColValue("Diagnosis").ConvertToDouble() != ' ' ? "table-row " : "none");

            html = html.Replace("{{chkClfindingflag}}", Bills.GetColValue("ClinicalFinding").ConvertToDouble() != ' ' ? "table-row " : "none");

            html = html.Replace("{{chkOprativeflag}}", Bills.GetColValue("OpertiveNotes").ConvertToDouble() != ' ' ? "table-row " : "none");

            html = html.Replace("{{chkTreatmentflag}}", Bills.GetColValue("TreatmentGiven").ConvertToDouble() != ' ' ? "table-row " : "none");


            html = html.Replace("{{chkInvestigationflag}}", Bills.GetColValue("Investigation").ConvertToDouble() != ' ' ? "table-row " : "none");


            return html;
        }

        /*   public bool Update(IPDDischargeSummaryParams IPDDischargeSummaryParams)
           {
               var disc = IPDDischargeSummaryParams.UpdateIPDDischargSummary.ToDictionary();
               ExecNonQueryProcWithOutSaveChanges("ps_Update_DischargeSummary_1", disc);
               _unitofWork.SaveChanges();
               return true;
           }*/
    }
}
