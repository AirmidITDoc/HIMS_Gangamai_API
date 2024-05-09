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
            ExecNonQueryProcWithOutSaveChanges("update_Admission_3", disc2);

            var vDischargeBedRelease = IPDischargeParams.DischargeBedRelease.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_DischargeBedRelease", vDischargeBedRelease);

            _unitofWork.SaveChanges();
            return DischargeId;

        }

        public bool Update(IPDischargeParams IPDischargeParams)
        {
            // new NotImplementedException();

            var disc4 = IPDischargeParams.UpdateIPDDischarg.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Discharge_1", disc4);

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
            var Bills = GetDataTableProc("rptDischargeCheckOutSlip", para);
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            
            html = html.Replace("{{BillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{DischargeTime}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

            html = html.Replace("{{OPD_IPD_ID}}", Bills.GetColValue("OPD_IPD_ID"));
            html = html.Replace("{{BillingUserName}}", Bills.GetColValue("BillingUserName"));
                          

            return html;
        }
    }
}

