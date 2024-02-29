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
                user.IsPOVerify= Convert.ToBoolean(reader["LoginStatus"]);
                user.IsGRNVerify = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsCollection = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsBedStatus = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsCurrentStk = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsPatientInfo = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsDateInterval = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsDateIntervalDays = Convert.ToInt64(reader["IsDateIntervalDays"]);
                user.MailId = reader["MailId"].ToString();
                user.MailDomain = reader["MailDomain"].ToString();
                user.LoginStatus = Convert.ToBoolean(reader["LoginStatus"]);
                user.AddChargeIsDelete = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsIndentVerify = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsPOInchargeVerify = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsRefDocEditOpt = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsInchIndVfy = Convert.ToBoolean(reader["LoginStatus"]);
                user.IsPharBalClearnace = Convert.ToBoolean(reader["LoginStatus"]);
            }
            reader.Close();
            return user;
        }

      
    }

}
