using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_OpdAppointment : GenericRepository, I_OpdAppointment
    {

        public R_OpdAppointment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
         
        public String Save(OpdAppointmentParams opdAppointmentParams)
        {
            //Add registration
            var outputId = new SqlParameter
            {
              SqlDbType = SqlDbType.BigInt,
             ParameterName = "@RegID",
            Value = 0,
            Direction = ParameterDirection.Output
            };

            //Add registration
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@VisitId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var dic = opdAppointmentParams.RegistrationSave.ToDictionary();
            dic.Remove("RegID");
            var RegID=ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", dic, outputId);
            
             opdAppointmentParams.VisitSave.RegID = Convert.ToInt64(RegID);
             var dic1 = opdAppointmentParams.VisitSave.ToDictionary();
               dic1.Remove("VisitId");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("insert_VisitDetails_New_1", dic1, outputId1);

            // insert_Registration_1_1
            // var VisitID = ExecNonQueryProcWithOutSaveChanges("ps_Insert_VisitDetails", dic, outputId);
            //add SMS ,,,insert_VisitDetails_New_1
            //dic = new Dictionary<string, object>
            //{
            //    { "RegId", registerId }
            //};

            //ExecNonQueryProcWithOutSaveChanges("SMSRegistraion", dic);

            //
            //dic = new Dictionary<string, object>
            //{
            //    { "PatVisitID", Convert.ToInt64(visitId) }
            //};

            //new code
            opdAppointmentParams.TokenNumberWithDoctorWiseSave.PatVisitID = Convert.ToInt64(VisitID);
            var disc3 = opdAppointmentParams.TokenNumberWithDoctorWiseSave.ToDictionary();
           ExecNonQueryProcWithOutSaveChanges("Insert_TokenNumberWithDoctorWise",disc3);
           
            //  Insert_TokenNumberWithDoctorWise
            //commit transaction
            _unitofWork.SaveChanges();
            
            return VisitID;
        }

        public string SavewithPhoto(OpdAppointmentParams opdAppointmentParams)
        {
            // throw new NotImplementedException();

            //Add registration
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            //Add registration
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@VisitId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var dic = opdAppointmentParams.RegistrationSavewithPhoto.ToDictionary();
            dic.Remove("RegID");
            dic.Remove("ImgFile");
            var RegID = ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", dic, outputId);

            opdAppointmentParams.VisitSave.RegID = Convert.ToInt64(RegID);
            var dic1 = opdAppointmentParams.VisitSave.ToDictionary();
            dic1.Remove("VisitId");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("insert_VisitDetails_New_1", dic1, outputId1);

            // insert_Registration_1_1
            // var VisitID = ExecNonQueryProcWithOutSaveChanges("ps_Insert_VisitDetails", dic, outputId);
            //add SMS ,,,insert_VisitDetails_New_1
            //dic = new Dictionary<string, object>
            //{
            //    { "RegId", registerId }
            //};

            //ExecNonQueryProcWithOutSaveChanges("SMSRegistraion", dic);

            //
            //dic = new Dictionary<string, object>
            //{
            //    { "PatVisitID", Convert.ToInt64(visitId) }
            //};

            //new code
            opdAppointmentParams.TokenNumberWithDoctorWiseSave.PatVisitID = Convert.ToInt64(VisitID);
            var disc3 = opdAppointmentParams.TokenNumberWithDoctorWiseSave.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_TokenNumberWithDoctorWise", disc3);

            //  Insert_TokenNumberWithDoctorWise
            //commit transaction
            _unitofWork.SaveChanges();

            return VisitID;
        }

        public bool Update(OpdAppointmentParams opdAppointmentParams)
        {
            //throw new NotImplementedException();

            //Update registration
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@VisitId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = opdAppointmentParams.RegistrationUpdate.ToDictionary();
            //dic.Remove("RegId");
            ExecNonQueryProcWithOutSaveChanges("update_RegForAppointment_1", dic);
            // update_RegForAppointment_1
            //add visit

          
            var Reg = opdAppointmentParams.RegistrationUpdate.RegId;
            opdAppointmentParams.VisitUpdate.RegID = Convert.ToInt64(Reg);
            var dic1 = opdAppointmentParams.VisitUpdate.ToDictionary();
            dic1.Remove("VisitId");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("insert_VisitDetails_New_1", dic1, outputId);

            // insert_Registration_1_1
            // var VisitID = ExecNonQueryProcWithOutSaveChanges("ps_Insert_VisitDetails", dic, outputId);
            //add SMS ,,,insert_VisitDetails_New_1
            //dic = new Dictionary<string, object>
            //{
            //    { "RegId", registerId }
            //};

            //ExecNonQueryProcWithOutSaveChanges("SMSRegistraion", dic);

            //
          /*  dic = new Dictionary<string, object>
            {
                { "PatVisitID", Convert.ToInt64(VisitID) }
            };*/

          opdAppointmentParams.TokenNumberWithDoctorWiseUpdate.PatVisitID = Convert.ToInt64(VisitID);
            var dic3 = opdAppointmentParams.TokenNumberWithDoctorWiseUpdate.ToDictionary();
            dic1.Remove("PatVisitID");
            ExecNonQueryProcWithOutSaveChanges("Insert_TokenNumberWithDoctorWise", dic3);
            //  Insert_TokenNumberWithDoctorWise
            //commit transaction
            _unitofWork.SaveChanges();

            return true;
        }

        public string ViewpatientAppointmentReceipt(int VisitId, string htmlFilePath, string htmlHeaderFilePath)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@VisitId", VisitId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptAppointmentPrint1", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;


            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));

            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("MobileNo"));

            html = html.Replace("{{DOT}}", Bills.GetColValue("DOT").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));

            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));

           
            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RefDoctorName}}", Bills.GetColValue("RefDoctorName"));

            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            html = html.Replace("{{RelativeName}}", Bills.GetColValue("RelativeName"));
            html = html.Replace("{{RelativePhoneNo}}", Bills.GetColValue("RelativePhoneNo"));

            html = html.Replace("{{RelationshipName}}", Bills.GetColValue("RelationshipName"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{IsMLC}}", Bills.GetColValue("IsMLC"));
            html = html.Replace("{{AdmittedDoctor1}}", Bills.GetColValue("AdmittedDoctor1"));

            html = html.Replace("{{AdmittedDoctor2}}", Bills.GetColValue("AdmittedDoctor2"));
            html = html.Replace("{{LoginUserSurname}}", Bills.GetColValue("LoginUserSurname"));

            return html;

        }
    }
}
