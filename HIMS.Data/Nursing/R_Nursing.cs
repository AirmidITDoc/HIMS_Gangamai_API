using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.CustomerInformation;
using HIMS.Data.Nursing;
using HIMS.Common.Utility;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using HIMS.Model.Opd;

namespace HIMS.Data.Nursing
{
    public class R_Nursing : GenericRepository, I_Nursing
    {
        public R_Nursing(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool SaveMTemplateMaster(MTemplateMasterParam MTemplateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MTemplateMasterParam.SaveMTemplateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_TemplateMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMTemplateMaster(MTemplateMasterParam MTemplateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MTemplateMasterParam.UpdateMTemplateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_TemplateMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelMTemplateMaster(MTemplateMasterParam MTemplateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MTemplateMasterParam.CancelMTemplateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_cancel_M_TemplateMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveTNursingNotes(NursingNoteParam NursingNoteParam)
        {
            // throw new NotImplementedException();
            var disc = NursingNoteParam.SaveNursingNoteParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_T_Nursing_Note", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateTNursingNotes(NursingNoteParam NursingNoteParam)
        {
            // throw new NotImplementedException();
            var disc = NursingNoteParam.UpdateNursingNoteParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_Nursing_Note", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingPatientHandoverParam.SaveTNursingPatientHandoverParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_Nursing_PatientHandover", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingPatientHandoverParam.UpdateTNursingPatientHandoverParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_Nursing_PatientHandover", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
          
            // throw new NotImplementedException();
            //var disc = TNursingMedicationChartParam.SaveTNursingMedicationChartParams.ToDictionary();
            //ExecNonQueryProcWithOutSaveChanges("m_insert_T_Nursing_MedicationChart", disc);
            foreach (var a in TNursingMedicationChartParam.SaveTNursingMedicationChartParams)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_Nursing_MedicationChart", disc);
            }
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingMedicationChartParam.UpdateTNursingMedicationChartParams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_Nursing_MedicationChart", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingMedicationChartParam.CancelTNursingMedicationChartParams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_T_Nursing_MedicationChart", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public string ViewNursingNotes(int AdmId, string htmlFilePath, string htmlHeader)
        {


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmId", AdmId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rpt_T_NursingNotesPrint", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["NursingNotes"].ConvertToString()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
         //   html = html.Replace("{{AdmId}}", Bills.GetColValue("AdmId"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));

            return html;
        }
        public string ViewDoctorNotes(int AdmID, string htmlFilePath, string htmlHeader)
        {


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmID", AdmID) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rpt_T_Doctors_Notes", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["DoctorsNotes"].ConvertToString()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{AdmID}}", Bills.GetColValue("AdmId"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{TDate}}", Bills.GetColValue("TDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));

            return html;
        }
        public string ViewNursingPatientHandover(int AdmId, string htmlFilePath, string htmlHeader)
        {


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmId", AdmId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rpt_T_Nursing_PatientHandover", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ShiftInfo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_I"].ConvertToString()).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_S"].ConvertToString()).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_B"].ConvertToString()).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_A"].ConvertToString()).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_R"].ConvertToString()).Append("</td></tr>");



            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{AdmId}}", Bills.GetColValue("AdmId"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
           

            return html;
        }
    }
       
}

