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

        public bool AppointmentCancle(OpdAppointmentParams opdAppointmentParams)
        {
            //  throw new NotImplementedException();
            
            var disc3 = opdAppointmentParams.Appointmentcancle.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancle_Appointment", disc3);
            _unitofWork.SaveChanges();
            return true;
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
                ParameterName = "@VisitID",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var dic = opdAppointmentParams.RegistrationSave.ToDictionary();
            dic.Remove("RegID");
            var RegID=ExecNonQueryProcWithOutSaveChanges("m_insert_Registration_1", dic, outputId);
            
             opdAppointmentParams.VisitSave.RegId = Convert.ToInt64(RegID);
             var dic1 = opdAppointmentParams.VisitSave.ToDictionary();
               dic1.Remove("VisitID");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("m_insert_VisitDetails_1", dic1, outputId1);

        
            //new code
            opdAppointmentParams.TokenNumberWithDoctorWiseSave.PatVisitID = Convert.ToInt64(VisitID);
            var disc3 = opdAppointmentParams.TokenNumberWithDoctorWiseSave.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_TokenNumber_DoctorWise", disc3);
           
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
            var RegID = ExecNonQueryProcWithOutSaveChanges("m_insert_Registration_1", dic, outputId);

            opdAppointmentParams.VisitSave.RegId = Convert.ToInt64(RegID);
            var dic1 = opdAppointmentParams.VisitSave.ToDictionary();
            dic1.Remove("VisitId");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("m_insert_VisitDetails_1", dic1, outputId1);

            //new code
            opdAppointmentParams.TokenNumberWithDoctorWiseSave.PatVisitID = Convert.ToInt64(VisitID);
            var disc3 = opdAppointmentParams.TokenNumberWithDoctorWiseSave.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_TokenNumber_DoctorWise", disc3);

            //  Insert_TokenNumberWithDoctorWise
            //commit transaction
            _unitofWork.SaveChanges();

            return VisitID;
        }

        public string Update(OpdAppointmentParams opdAppointmentParams)
        {
            //throw new NotImplementedException();

            //Update registration
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@VisitID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = opdAppointmentParams.RegistrationUpdate.ToDictionary();
            //dic.Remove("RegId");
            ExecNonQueryProcWithOutSaveChanges("m_update_RegistrationForAppointment_1", dic);
          
            //add visit
            var Reg = opdAppointmentParams.RegistrationUpdate.RegId;
            opdAppointmentParams.VisitSave.RegId = Convert.ToInt64(Reg);
            var dic1 = opdAppointmentParams.VisitSave.ToDictionary();
            dic1.Remove("VisitID");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("m_insert_VisitDetails_1", dic1, outputId);

            
           opdAppointmentParams.TokenNumberWithDoctorWiseSave.PatVisitID = Convert.ToInt64(VisitID);
            var dic3 = opdAppointmentParams.TokenNumberWithDoctorWiseSave.ToDictionary();
            dic1.Remove("PatVisitID");
            ExecNonQueryProcWithOutSaveChanges("m_Insert_TokenNumber_DoctorWise", dic3);
            //commit transaction
            _unitofWork.SaveChanges(); 

            return VisitID;
        }

        public bool UpdateVitalInformation(OpdAppointmentParams opdAppointmentParams)
        {
            //  throw new NotImplementedException();

            var disc3 = opdAppointmentParams.UpdateVitalInformation.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_vitalInformation", disc3);
            _unitofWork.SaveChanges();
            return true;
        }
        public string ViewOppatientAppointmentdetailsReceipt(int VisitId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@VisitId", VisitId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptAppointmentPrint1", para);
            string html = File.ReadAllText(htmlFilePath);
           
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;


            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{VisitDate}}", Bills.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{ConsultantDoctorName}}", Bills.GetColValue("ConsultantDoctorName"));
            
                html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{Expr1}}", Bills.GetColValue("Expr1"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("MobileNo"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));


            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            html = html.Replace("{{RelativeName}}", Bills.GetColValue("RelativeName"));
            html = html.Replace("{{RelativePhoneNo}}", Bills.GetColValue("RelativePhoneNo"));

            html = html.Replace("{{RelationshipName}}", Bills.GetColValue("RelationshipName"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{IsMLC}}", Bills.GetColValue("IsMLC"));
           
            html = html.Replace("{{AdmittedDoctor2}}", Bills.GetColValue("AdmittedDoctor2"));
            html = html.Replace("{{LoginUserSurname}}", Bills.GetColValue("LoginUserSurname"));
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{Pulse}}", Bills.GetColValue("Pulse"));
            html = html.Replace("{{Height}}", Bills.GetColValue("Height"));
            html = html.Replace("{{Weight}}", Bills.GetColValue("PWeight"));
            html = html.Replace("{{Temp}}", Bills.GetColValue("Temp"));
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{BSL}}", Bills.GetColValue("BSL"));
            html = html.Replace("{{BMI}}", Bills.GetColValue("BMI"));
            html = html.Replace("{{SpO2}}", Bills.GetColValue("SpO2"));



            html = html.Replace("{{chkBPflag}}", Bills.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkPulseflag}}", Bills.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkHeightflag}}", Bills.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkWeightflag}}", Bills.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkTempflag}}", Bills.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBSLflag}}", Bills.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBMIflag}}", Bills.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkSpO2flag}}", Bills.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");



            return html;
        }
        public string ViewOPDSpineCasePaper(int VisitId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@VisitId", VisitId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptAppointmentPrint1", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;


            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{VisitDate}}", Bills.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{ConsultantDoctorName}}", Bills.GetColValue("ConsultantDoctorName"));

            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{Expr1}}", Bills.GetColValue("Expr1"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("MobileNo"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));


            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            html = html.Replace("{{RelativeName}}", Bills.GetColValue("RelativeName"));
            html = html.Replace("{{RelativePhoneNo}}", Bills.GetColValue("RelativePhoneNo"));

            html = html.Replace("{{RelationshipName}}", Bills.GetColValue("RelationshipName"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{IsMLC}}", Bills.GetColValue("IsMLC"));

            html = html.Replace("{{AdmittedDoctor2}}", Bills.GetColValue("AdmittedDoctor2"));
            html = html.Replace("{{LoginUserSurname}}", Bills.GetColValue("LoginUserSurname"));
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{Pulse}}", Bills.GetColValue("Pulse"));
            html = html.Replace("{{Height}}", Bills.GetColValue("Height"));
            html = html.Replace("{{Weight}}", Bills.GetColValue("PWeight"));
            html = html.Replace("{{Temp}}", Bills.GetColValue("Temp"));
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{BSL}}", Bills.GetColValue("BSL"));
            html = html.Replace("{{BMI}}", Bills.GetColValue("BMI"));
            html = html.Replace("{{SpO2}}", Bills.GetColValue("SpO2"));



            html = html.Replace("{{chkBPflag}}", Bills.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkPulseflag}}", Bills.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkHeightflag}}", Bills.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkWeightflag}}", Bills.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkTempflag}}", Bills.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBSLflag}}", Bills.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBMIflag}}", Bills.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkSpO2flag}}", Bills.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");



            return html;
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
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
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
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{Pulse}}", Bills.GetColValue("Pulse"));
            html = html.Replace("{{Height}}", Bills.GetColValue("Height"));
            html = html.Replace("{{Weight}}", Bills.GetColValue("PWeight"));
            html = html.Replace("{{Temp}}", Bills.GetColValue("Temp"));
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{BSL}}", Bills.GetColValue("BSL"));
            html = html.Replace("{{BMI}}", Bills.GetColValue("BMI"));
            html = html.Replace("{{SpO2}}", Bills.GetColValue("SpO2"));



            html = html.Replace("{{chkBPflag}}", Bills.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkPulseflag}}", Bills.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkHeightflag}}", Bills.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkWeightflag}}", Bills.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkTempflag}}", Bills.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBSLflag}}", Bills.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBMIflag}}", Bills.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkSpO2flag}}", Bills.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");

            return html;

        }

        public DataTable GetDataForReport(int VisitId)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@VisitId", VisitId) { DbType = DbType.Int64 };
            return GetDataTableProc("rptAppointmentPrint1", para);
        }
        public string ViewAppointmentTemplate(DataTable Bills, string htmlFilePath, string htmlHeade)
        {

            //string html = File.ReadAllText(htmlFilePath);

            string html = htmlFilePath;


            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
          //html = html.Replace("{{NewHeader}}", htmlHeader);

            html = html.Replace("{{DataContent}}", htmlHeade);
           //html = html.Replace("{{NewHeader}}", Hospitalheader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{VisitDate}}", Bills.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy  hh:mm tt"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{ConsultantDoctorName}}", Bills.GetColValue("ConsultantDoctorName"));

            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{Expr1}}", Bills.GetColValue("Expr1"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("MobileNo"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));


            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            html = html.Replace("{{RelativeName}}", Bills.GetColValue("RelativeName"));
            html = html.Replace("{{RelativePhoneNo}}", Bills.GetColValue("RelativePhoneNo"));

            html = html.Replace("{{RelationshipName}}", Bills.GetColValue("RelationshipName"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{IsMLC}}", Bills.GetColValue("IsMLC"));

            html = html.Replace("{{AdmittedDoctor2}}", Bills.GetColValue("AdmittedDoctor2"));
            html = html.Replace("{{LoginUserSurname}}", Bills.GetColValue("LoginUserSurname"));

        
            html = html.Replace("{{chkyearflag}}", Bills.GetColValue("AgeYear").ToInt() == 0 ? "none" : "visible");
            html = html.Replace("{{chkmonthflag}}", Bills.GetColValue("AgeMonth").ToInt() == 0 ? "none" : "visible");
            html = html.Replace("{{chkdayflag}}", Bills.GetColValue("AgeDay").ToInt() == 0 ? "none" : "visible");


            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{Pulse}}", Bills.GetColValue("Pulse"));
            html = html.Replace("{{Height}}", Bills.GetColValue("Height"));
            html = html.Replace("{{Weight}}", Bills.GetColValue("PWeight"));
            html = html.Replace("{{Temp}}", Bills.GetColValue("Temp"));
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{BSL}}", Bills.GetColValue("BSL"));
            html = html.Replace("{{BMI}}", Bills.GetColValue("BMI"));
            html = html.Replace("{{SpO2}}", Bills.GetColValue("SpO2"));

          

            html = html.Replace("{{chkBPflag}}", Bills.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkPulseflag}}", Bills.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkHeightflag}}", Bills.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkWeightflag}}", Bills.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkTempflag}}", Bills.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBSLflag}}", Bills.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBMIflag}}", Bills.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkSpO2flag}}", Bills.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");


            return html;
        }
      
       
    }
}
