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

            var DischargesummaryId =ExecNonQueryProcWithOutSaveChanges("m_insert_DischargeSummary_1", disc, outputId);

            foreach (var a in IPDDischargeSummaryParams.InsertIPPrescriptionDischarge)
            {
                var vPrescDisc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_IP_Prescription_Discharge_1", vPrescDisc);
            }

            _unitofWork.SaveChanges();
            return DischargesummaryId;
        }

        public bool Update(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {
          //  throw new NotImplementedException();

            var disc3 = IPDDischargeSummaryParams.UpdateIPDDischargSummary.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_DischargeSummary_1", disc3);

            var vDeletePres = IPDDischargeSummaryParams.DeleteIPPrescriptionDischarge.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Delete_T_IP_Prescription_Discharge", vDeletePres);

            foreach (var a in IPDDischargeSummaryParams.InsertIPPrescriptionDischarge)
            {
                var vPrescDisc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_IP_Prescription_Discharge_1", vPrescDisc);
            }

            _unitofWork.SaveChanges();
            return true;
        }



        public String DischTemplateInsert(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DischargesummaryId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc = IPDDischargeSummaryParams.InsertIPDDischargSummarytemplate.ToDictionary();
            disc.Remove("DischargesummaryId");

            var DischargesummaryId = ExecNonQueryProcWithOutSaveChanges("m_insert_DischargeSummaryTemplate", disc, outputId);

            foreach (var a in IPDDischargeSummaryParams.InsertIPPrescriptionDischarge)
            {
                var vPrescDisc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_IP_Prescription_Discharge_1", vPrescDisc);
            }

            _unitofWork.SaveChanges();
            return DischargesummaryId;
        } 

        public bool DischTemplateUpdate(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {
            //  throw new NotImplementedException();

            var disc3 = IPDDischargeSummaryParams.UpdatetIPDDischargSummarytemplate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_DischargeSummaryTemplate", disc3);

            var vDeletePres = IPDDischargeSummaryParams.DeleteIPPrescriptionDischarge.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Delete_T_IP_Prescription_Discharge", vDeletePres);

            foreach (var a in IPDDischargeSummaryParams.InsertIPPrescriptionDischarge)
            {
                var vPrescDisc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_IP_Prescription_Discharge_1", vPrescDisc);
            }

            _unitofWork.SaveChanges();
            return true;
        }
        public DataTable GetDataForReport(int AdmissionID)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            return GetDataTableProc("m_rptDischargeSummaryPrint_New", para);
        }
        public string ViewDischargeSummary(DataTable Bills,int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            //  throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
         
            var Bills1 = GetDataTableProc("m_Rtrv_IP_Prescription_Discharge", para);
            int length = 0;
            length = Bills1.Rows.Count;
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{DischargeTime}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{Followupdate}}", Bills.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{History}}", Bills.GetColValue("History"));
            html = html.Replace("{{Diagnosis}}", Bills.GetColValue("Diagnosis"));
            html = html.Replace("{{ClinicalFinding}}", Bills.GetColValue("ClinicalFinding"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{OpertiveNotes}}", Bills.GetColValue("OpertiveNotes"));
            html = html.Replace("{{TreatmentGiven}}", Bills.GetColValue("TreatmentGiven"));
            html = html.Replace("{{Investigation}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{p}}", Bills.GetColValue("History"));
            html = html.Replace("{{R}}", Bills.GetColValue("Diagnosis"));
            html = html.Replace("{{SPO2}}", Bills.GetColValue("ClinicalFinding"));
            html = html.Replace("{{RS}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{PA}}", Bills.GetColValue("OpertiveNotes"));
            html = html.Replace("{{CVS}}", Bills.GetColValue("TreatmentGiven"));
            html = html.Replace("{{CNS}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{DischargeTypeName}}", Bills.GetColValue("DischargeTypeName"));
            html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", Bills.GetColValue("TreatmentAdvisedAfterDischarge"));

            html = html.Replace("{{ClinicalConditionOnAdmisssion}}", Bills.GetColValue("ClinicalConditionOnAdmisssion"));

            html = html.Replace("{{OtherConDrOpinions}}", Bills.GetColValue("OtherConDrOpinions"));

            html = html.Replace("{{PainManagementTechnique}}", Bills.GetColValue("PainManagementTechnique"));
            html = html.Replace("{{LifeStyle}}", Bills.GetColValue("LifeStyle"));

            html = html.Replace("{{WarningSymptoms}}", Bills.GetColValue("WarningSymptoms"));
            html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", Bills.GetColValue("ConditionAtTheTimeOfDischarge"));




            html = html.Replace("{{DoctorAssistantName}}", Bills.GetColValue("DoctorAssistantName"));
            html = html.Replace("{{PreOthNumber}}", Bills.GetColValue("PreOthNumber"));
            html = html.Replace("{{SurgeryProcDone}}", Bills.GetColValue("SurgeryProcDone"));
            html = html.Replace("{{ICD10CODE}}", Bills.GetColValue("ICD10CODE"));
            html = html.Replace("{{Radiology}}", Bills.GetColValue("Radiology"));


            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDoctorName}}", Bills.GetColValue("RefDoctorName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{AddedBy}}", Bills.GetColValue("AddedBy"));

            //border: 1px solid #d4c3c3;

            foreach (DataRow dr in Bills1.Rows)
            {
                i++;
                items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


                items.Append("<tr style=\"text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());



            //html = html.Replace("{{OpertiveNotes}}", Bills.GetColValue("OpertiveNotes"));
            //html = html.Replace("{{TreatmentGiven}}", Bills.GetColValue("TreatmentGiven"));
            //html = html.Replace("{{Investigation}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{chkHistoryflag}}", Bills.GetColValue("History").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkHistoryflag}}", Bills.GetColValue("History").ConvertToString() != "" ? "block" : "table-row");



            html = html.Replace("{{chkDignosflag}}", Bills.GetColValue("Diagnosis").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkClfindingflag}}", Bills.GetColValue("ClinicalFinding").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkOprativeflag}}", Bills.GetColValue("OpertiveNotes").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkTreatmentflag}}", Bills.GetColValue("TreatmentGiven").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkInvestigationflag}}", Bills.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");



            html = html.Replace("{{chktreatadviceflag}}", Bills.GetColValue("TreatmentAdvisedAfterDischarge").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkTreatmentflag}}", Bills.GetColValue("TreatmentGiven").ConvertToString() != " " ? "table-row" : "none");


            html = html.Replace("{{chkInvestigationflag}}", Bills.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");
            //
            html = html.Replace("{{chkClinicalconditionflag}}", Bills.GetColValue("ClinicalConditionOnAdmisssion").ConvertToString() != "" ? "table-row" : "none");



            html = html.Replace("{{chkotherconditionflag}}", Bills.GetColValue("OtherConDrOpinions").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkpainmanageflag}}", Bills.GetColValue("PainManagementTechnique").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkConondiscflag}}", Bills.GetColValue("ConditionAtTheTimeOfDischarge").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkLifeStyleflag}}", Bills.GetColValue("LifeStyle").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkWarningSymptomsflag}}", Bills.GetColValue("WarningSymptoms").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkSurgeryProcDoneflag}}", Bills.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
            html = html.Replace("{{chkidischragetypeflag}}", Bills.GetColValue("DischargeTypeName").ConvertToString() != "NULL" ? "table-row" : "none");


            html = html.Replace("{{chkSurgeryProcDoneflag}}", Bills.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
            html = html.Replace("{{chkSurgeryPrescriptionflag}}", length != 0 ? "table-row" : "none");




            return html;
        }



        public string ViewDischargeSummaryTemplate(DataTable Bills, int AdmissionID, string DIschargetemplate, string htmlHeader)
        {
            //  throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };

            var Bills1 = GetDataTableProc("m_Rtrv_IP_Prescription_Discharge", para);
            int length = 0;
            length = Bills1.Rows.Count;
            string html = DIschargetemplate;
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{DischargeTime}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{Followupdate}}", Bills.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{History}}", Bills.GetColValue("History"));
            html = html.Replace("{{Diagnosis}}", Bills.GetColValue("Diagnosis"));
            html = html.Replace("{{ClinicalFinding}}", Bills.GetColValue("ClinicalFinding"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{OpertiveNotes}}", Bills.GetColValue("OpertiveNotes"));
            html = html.Replace("{{TreatmentGiven}}", Bills.GetColValue("TreatmentGiven"));
            html = html.Replace("{{Investigation}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{p}}", Bills.GetColValue("History"));
            html = html.Replace("{{R}}", Bills.GetColValue("Diagnosis"));
            html = html.Replace("{{SPO2}}", Bills.GetColValue("ClinicalFinding"));
            html = html.Replace("{{RS}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{PA}}", Bills.GetColValue("OpertiveNotes"));
            html = html.Replace("{{CVS}}", Bills.GetColValue("TreatmentGiven"));
            html = html.Replace("{{CNS}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{DischargeTypeName}}", Bills.GetColValue("DischargeTypeName"));
            html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", Bills.GetColValue("TreatmentAdvisedAfterDischarge"));

            html = html.Replace("{{ClinicalConditionOnAdmisssion}}", Bills.GetColValue("ClinicalConditionOnAdmisssion"));

            html = html.Replace("{{OtherConDrOpinions}}", Bills.GetColValue("OtherConDrOpinions"));

            html = html.Replace("{{PainManagementTechnique}}", Bills.GetColValue("PainManagementTechnique"));
            html = html.Replace("{{LifeStyle}}", Bills.GetColValue("LifeStyle"));

            html = html.Replace("{{WarningSymptoms}}", Bills.GetColValue("WarningSymptoms"));
            html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", Bills.GetColValue("ConditionAtTheTimeOfDischarge"));




            html = html.Replace("{{DoctorAssistantName}}", Bills.GetColValue("DoctorAssistantName"));
            html = html.Replace("{{PreOthNumber}}", Bills.GetColValue("PreOthNumber"));
            html = html.Replace("{{SurgeryProcDone}}", Bills.GetColValue("SurgeryProcDone"));
            html = html.Replace("{{ICD10CODE}}", Bills.GetColValue("ICD10CODE"));
            html = html.Replace("{{Radiology}}", Bills.GetColValue("Radiology"));


            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDoctorName}}", Bills.GetColValue("RefDoctorName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{AddedBy}}", Bills.GetColValue("AddedBy"));

            //border: 1px solid #d4c3c3;

            foreach (DataRow dr in Bills1.Rows)
            {
                i++;
                items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


                items.Append("<tr style=\"text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());



            //html = html.Replace("{{OpertiveNotes}}", Bills.GetColValue("OpertiveNotes"));
            //html = html.Replace("{{TreatmentGiven}}", Bills.GetColValue("TreatmentGiven"));
            //html = html.Replace("{{Investigation}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{chkHistoryflag}}", Bills.GetColValue("History").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkHistoryflag}}", Bills.GetColValue("History").ConvertToString() != "" ? "block" : "table-row");



            html = html.Replace("{{chkDignosflag}}", Bills.GetColValue("Diagnosis").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkClfindingflag}}", Bills.GetColValue("ClinicalFinding").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkOprativeflag}}", Bills.GetColValue("OpertiveNotes").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkTreatmentflag}}", Bills.GetColValue("TreatmentGiven").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkInvestigationflag}}", Bills.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");



            html = html.Replace("{{chktreatadviceflag}}", Bills.GetColValue("TreatmentAdvisedAfterDischarge").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkTreatmentflag}}", Bills.GetColValue("TreatmentGiven").ConvertToString() != " " ? "table-row" : "none");


            html = html.Replace("{{chkInvestigationflag}}", Bills.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");
            //
            html = html.Replace("{{chkClinicalconditionflag}}", Bills.GetColValue("ClinicalConditionOnAdmisssion").ConvertToString() != "" ? "table-row" : "none");



            html = html.Replace("{{chkotherconditionflag}}", Bills.GetColValue("OtherConDrOpinions").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkpainmanageflag}}", Bills.GetColValue("PainManagementTechnique").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkConondiscflag}}", Bills.GetColValue("ConditionAtTheTimeOfDischarge").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkLifeStyleflag}}", Bills.GetColValue("LifeStyle").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkWarningSymptomsflag}}", Bills.GetColValue("WarningSymptoms").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkSurgeryProcDoneflag}}", Bills.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
            html = html.Replace("{{chkidischragetypeflag}}", Bills.GetColValue("DischargeTypeName").ConvertToString() != "NULL" ? "table-row" : "none");


            html = html.Replace("{{chkSurgeryProcDoneflag}}", Bills.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
            html = html.Replace("{{chkSurgeryPrescriptionflag}}", length != 0 ? "table-row" : "none");




            return html;
        }



        public string ViewDischargeSummaryold(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptDischargeSummaryPrint_New", para);


            var Bills1 = GetDataTableProc("m_Rtrv_IP_Prescription_Discharge", para);
            int length = 0;
            length = Bills1.Rows.Count;
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{DischargeTime}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{Followupdate}}", Bills.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{History}}", Bills.GetColValue("History"));
            html = html.Replace("{{Diagnosis}}", Bills.GetColValue("Diagnosis"));
            html = html.Replace("{{ClinicalFinding}}", Bills.GetColValue("ClinicalFinding"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{OpertiveNotes}}", Bills.GetColValue("OpertiveNotes"));
            html = html.Replace("{{TreatmentGiven}}", Bills.GetColValue("TreatmentGiven"));
            html = html.Replace("{{Investigation}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{p}}", Bills.GetColValue("History"));
            html = html.Replace("{{R}}", Bills.GetColValue("Diagnosis"));
            html = html.Replace("{{SPO2}}", Bills.GetColValue("ClinicalFinding"));
            html = html.Replace("{{RS}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{PA}}", Bills.GetColValue("OpertiveNotes"));
            html = html.Replace("{{CVS}}", Bills.GetColValue("TreatmentGiven"));
            html = html.Replace("{{CNS}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{DischargeTypeName}}", Bills.GetColValue("DischargeTypeName"));
            html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", Bills.GetColValue("TreatmentAdvisedAfterDischarge"));

            html = html.Replace("{{ClinicalConditionOnAdmisssion}}", Bills.GetColValue("ClinicalConditionOnAdmisssion"));

            html = html.Replace("{{OtherConDrOpinions}}", Bills.GetColValue("OtherConDrOpinions"));

            html = html.Replace("{{PainManagementTechnique}}", Bills.GetColValue("PainManagementTechnique"));
            html = html.Replace("{{LifeStyle}}", Bills.GetColValue("LifeStyle"));

            html = html.Replace("{{WarningSymptoms}}", Bills.GetColValue("WarningSymptoms"));
            html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", Bills.GetColValue("ConditionAtTheTimeOfDischarge"));




            html = html.Replace("{{DoctorAssistantName}}", Bills.GetColValue("DoctorAssistantName"));
            html = html.Replace("{{PreOthNumber}}", Bills.GetColValue("PreOthNumber"));
            html = html.Replace("{{SurgeryProcDone}}", Bills.GetColValue("SurgeryProcDone"));
            html = html.Replace("{{ICD10CODE}}", Bills.GetColValue("ICD10CODE"));
            html = html.Replace("{{Radiology}}", Bills.GetColValue("Radiology"));


            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDoctorName}}", Bills.GetColValue("RefDoctorName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{AddedBy}}", Bills.GetColValue("AddedBy"));

            //border: 1px solid #d4c3c3;

            foreach (DataRow dr in Bills1.Rows)
            {
                i++;
                items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


                items.Append("<tr style=\"text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());



            //html = html.Replace("{{OpertiveNotes}}", Bills.GetColValue("OpertiveNotes"));
            //html = html.Replace("{{TreatmentGiven}}", Bills.GetColValue("TreatmentGiven"));
            //html = html.Replace("{{Investigation}}", Bills.GetColValue("Investigation"));

            html = html.Replace("{{chkHistoryflag}}", Bills.GetColValue("History").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkHistoryflag}}", Bills.GetColValue("History").ConvertToString() != "" ? "block" : "table-row");



            html = html.Replace("{{chkDignosflag}}", Bills.GetColValue("Diagnosis").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkClfindingflag}}", Bills.GetColValue("ClinicalFinding").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkOprativeflag}}", Bills.GetColValue("OpertiveNotes").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkTreatmentflag}}", Bills.GetColValue("TreatmentGiven").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkInvestigationflag}}", Bills.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");



            html = html.Replace("{{chktreatadviceflag}}", Bills.GetColValue("TreatmentAdvisedAfterDischarge").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkTreatmentflag}}", Bills.GetColValue("TreatmentGiven").ConvertToString() != " " ? "table-row" : "none");


            html = html.Replace("{{chkInvestigationflag}}", Bills.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");
            //
            html = html.Replace("{{chkClinicalconditionflag}}", Bills.GetColValue("ClinicalConditionOnAdmisssion").ConvertToString() != "" ? "table-row" : "none");



            html = html.Replace("{{chkotherconditionflag}}", Bills.GetColValue("OtherConDrOpinions").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkpainmanageflag}}", Bills.GetColValue("PainManagementTechnique").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkConondiscflag}}", Bills.GetColValue("ConditionAtTheTimeOfDischarge").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkLifeStyleflag}}", Bills.GetColValue("LifeStyle").ConvertToString() != "" ? "table-row" : "none");

            html = html.Replace("{{chkWarningSymptomsflag}}", Bills.GetColValue("WarningSymptoms").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{chkSurgeryProcDoneflag}}", Bills.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
            html = html.Replace("{{chkidischragetypeflag}}", Bills.GetColValue("DischargeTypeName").ConvertToString() != "NULL" ? "table-row" : "none");


            html = html.Replace("{{chkSurgeryProcDoneflag}}", Bills.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
            html = html.Replace("{{chkSurgeryPrescriptionflag}}", length != 0 ? "table-row" : "none");




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
