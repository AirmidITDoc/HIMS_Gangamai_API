using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using HIMS.Common.Utility;
using HIMS.Model.CustomerInformation;
using HIMS.Data.CustomerInformation;
using HIMS.Data.OT;
using System.IO;

namespace HIMS.Data.OT
{
    public class R_OT : GenericRepository, I_OT
    {
        public R_OT(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool SaveOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingRequestParam.SaveOTBookingRequestParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_OTBooking_Request", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingRequestParam.UpdateOTBookingRequestParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_OTBooking_Request", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingRequestParam.CancelOTBookingRequestParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cancel_T_OTBooking_Request", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveOTBooking(OTBookingParam OTBookingParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingParam.SaveOTBookingParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_OTBooking", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateOTBooking(OTBookingParam OTBookingParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingParam.UpdateOTBookingParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_OTBooking", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelOTBooking(OTBookingParam OTBookingParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingParam.CancelOTBookingParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_T_OTBooking", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
       

        public bool SaveConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            // throw new NotImplementedException();
            var disc = ConsentMasterParam.SaveConsentMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_ConsentMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            // throw new NotImplementedException();
            var disc = ConsentMasterParam.UpdateConsentMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_ConsentMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            // throw new NotImplementedException();
            var disc = ConsentMasterParam.CancelConsentMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_M_ConsentMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveOTTableMaster(MOTTableMasterParam MOTTableMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTTableMasterParam.SaveOTTableMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_OT_TableMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateOTTableMaster(MOTTableMasterParam MOTTableMasterParam)
        {

            var disc3 = MOTTableMasterParam.UpdateOTTableMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_M_OT_TableMaster", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool CancelOTTableMaster(MOTTableMasterParam MOTTableMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTTableMasterParam.CancelOTTableMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_M_OT_TableMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTSurgeryCategoryMasterParam.SaveMOTSurgeryCategoryMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_OT_SurgeryCategoryMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam)
        {

            var disc3 = MOTSurgeryCategoryMasterParam.UpdateMOTSurgeryCategoryMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_M_OT_SurgeryCategoryMaster", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool CancelMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTSurgeryCategoryMasterParam.CancelMOTSurgeryCategoryMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_M_OT_SurgeryCategoryMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTTypeMasterParam.SaveMOTTypeMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_OT_TypeMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam)
        {

            var disc3 = MOTTypeMasterParam.UpdateOTTypemasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_M_OT_TypeMaster", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool CancelMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTTypeMasterParam.CancelOTTypeMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_M_OT_TypeMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTSiteDescriptionMasterParam.SaveMOTSiteDescriptionMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_OT_SiteDescriptionMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam)
        {

            var disc3 = MOTSiteDescriptionMasterParam.UpdateMOTSiteDescriptionMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_M_OT_SiteDescriptionMaster", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool CancelMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTSiteDescriptionMasterParam.CancelMOTSiteDescriptionMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_M_OT_SiteDescriptionMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTSurgeryMasterParam.SaveMOTSurgeryMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_OT_SurgeryMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam)
        {

            var disc3 = MOTSurgeryMasterParam.UpdateMOTSurgeryMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_M_OT_SurgeryMaster", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool CancelMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MOTSurgeryMasterParam.CancelMOTSurgeryMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_M_OT_SurgeryMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public string ViewTConsentInformation(int ConsentId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@ConsentId", ConsentId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rpt_TConsentInformation", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ConsentText"].ConvertToString()).Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{ConsentId}}", Bills.GetColValue("ConsentId"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{ConsentName}}", Bills.GetColValue("ConsentName"));


            //html = html.Replace("{{RequestId}}", Bills.GetColValue("RequestId"));
            //html = html.Replace("{{OPDNo}}", Bills.GetColValue("IPDNo"));
            //html = html.Replace("{{ReqDate}}", Bills.GetColValue("ReqDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            //html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));


            //html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));

            //html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{ConsentText}}", Bills.GetColValue("ConsentText"));
            //html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            //html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            //html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            //html = html.Replace("{{OP_IP_Type}}", Bills.GetColValue("OP_IP_Type"));
            //html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            //html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));


            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));



            return html;
        }
    }
}
