using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Opd
{
    public class R_LoginManager :I_LoginManager
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public R_LoginManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public Login Get(string userName)
        {

           // userName = "Admin";
            var user = new Login();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM LOGINMANAGER WHERE UserName=@userName";
            command.Parameters.AddWithValue("@UserName",userName);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user.UserName = reader["UserName"].ToString();
                user.Password = reader["Password"].ToString();
                user.FirstName = reader["FirstName"].ToString();
                user.LastName = reader["LastName"].ToString();
                user.UserId = Convert.ToInt64(reader["UserId"]);
                user.RoleId = Convert.ToInt64(reader["RoleId"]);
                user.StoreId = Convert.ToInt64(reader["StoreId"]);
                user.IsDoctorType = Convert.ToBoolean(reader["LoginStatus"]);
                user.DoctorID = Convert.ToInt64(reader["DoctorID"]);
                user.IsPOVerify= Convert.ToBoolean(reader["IsPOVerify"]);
                user.IsGRNVerify = Convert.ToBoolean(reader["IsGRNVerify"]);
                user.IsCollection = Convert.ToBoolean(reader["IsCollection"]);
                user.IsBedStatus = Convert.ToBoolean(reader["IsBedStatus"]);
                user.IsCurrentStk = Convert.ToBoolean(reader["IsCurrentStk"]);
                user.IsPatientInfo = Convert.ToBoolean(reader["IsPatientInfo"]);
                user.IsDateInterval = Convert.ToBoolean(reader["IsDateInterval"]);
                user.IsDateIntervalDays = Convert.ToInt64(reader["IsDateIntervalDays"]);
                user.MailId = reader["MailId"].ToString();
                user.MailDomain = reader["MailDomain"].ToString();
                user.LoginStatus = Convert.ToBoolean(reader["LoginStatus"]);
                user.AddChargeIsDelete = Convert.ToBoolean(reader["AddChargeIsDelete"]);
                user.IsIndentVerify = Convert.ToBoolean(reader["IsIndentVerify"]);
                user.IsPOInchargeVerify = Convert.ToBoolean(reader["IsPOInchargeVerify"]);
                user.IsRefDocEditOpt = Convert.ToBoolean(reader["IsRefDocEditOpt"]);
                user.IsInchIndVfy = Convert.ToBoolean(reader["IsInchIndVfy"]);
                //user.IsPharBalClearnace = Convert.ToBoolean(reader["IsPharBalClearnace"]);
                user.WebRoleId = Convert.ToInt64(reader["WebRoleId"]);
                user.PharOPOpt = Convert.ToBoolean(reader["PharOPOpt"]);
                user.PharIPOpt = Convert.ToBoolean(reader["PharIPOpt"]);
                user.PharExtOpt = Convert.ToBoolean(reader["PharExtOpt"]);
                user.IsDiscApply = Convert.ToInt64(reader["IsDiscApply"]);
                user.DiscApplyPer = Convert.ToInt64(reader["DiscApplyPer"]);
            }
            reader.Close();
            return user;
        }

      
    }

}
