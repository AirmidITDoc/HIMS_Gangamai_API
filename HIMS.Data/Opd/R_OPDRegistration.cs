using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Opd;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HIMS.Model.CustomerInformation;
using System.IO;


namespace HIMS.Data.Opd
{
    public class R_OPDRegistration :GenericRepository,I_OPDRegistration
    {
        public R_OPDRegistration(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }



       
    public bool TConsentInformationSave(TConsentInformationparams TConsentInformationparams)
        {
            // throw new NotImplementedException();
            var disc = TConsentInformationparams.SaveTConsentInformationparams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_ConsentInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool TConsentInformationUpdate(TConsentInformationparams TConsentInformationparams)
        {
            // throw new NotImplementedException();
            var disc = TConsentInformationparams.UpdateTConsentInformationparams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_ConsentInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool TCertificateInformationSave(TCertificateInformationparams TCertificateInformationparams)
        {
            // throw new NotImplementedException();
            var disc = TCertificateInformationparams.SaveTCertificateInformationparams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_CertificateInformation ", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool TCertificateInformationUpdate(TCertificateInformationparams TCertificateInformationparams)
        {
            // throw new NotImplementedException();
            var disc = TCertificateInformationparams.UpdateTCertificateInformationparams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_CertificateInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();

            return true;

        }


        public string Insert(OPDRegistrationParams OPDRegistrationParams)
        {
            //add registration
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = OPDRegistrationParams.OPDRegistrationSave.ToDictionary();
            dic.Remove("RegID");
            var RegID = ExecNonQueryProcWithOutSaveChanges("m_insert_Registration_1", dic, outputId);

           _unitofWork.SaveChanges();

            return RegID;
        }

        public bool Update(OPDRegistrationParams OPDRegistrationParams)
        {
            var disc1 = OPDRegistrationParams.OPDRegistrationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_RegForAppointment_1", disc1);

                       /*
            var disc = OPDRegistrationParams.OPDRegistrationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_Registration_1", disc);*/
            _unitofWork.SaveChanges();
            return true;
            
            
        }
        public string ViewCertificateInformationPrint(int CertificateId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@CertificateId", CertificateId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rpt_CertificateInformationPrint", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["CertificateText"].ConvertToString()).Append("</td></tr>");
      

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{CertificateId}}", Bills.GetColValue("CertificateId"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{CertificateName}}", Bills.GetColValue("CertificateName"));

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{RequestId}}", Bills.GetColValue("RequestId"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{ReqDate}}", Bills.GetColValue("ReqDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{CreatedDatetime}}", Bills.GetColValue("CreatedDatetime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{CertificateText}}", Bills.GetColValue("CertificateText"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{OP_IP_Type}}", Bills.GetColValue("OP_IP_Type"));
            html = html.Replace("{{RefDoctorName}}", Bills.GetColValue("RefDoctorName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));


            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));




            return html;
        }
    }

}
    
