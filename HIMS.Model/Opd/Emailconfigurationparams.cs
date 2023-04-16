using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class Emailconfigurationparams
    {
        public InsertEmailconfiguration InsertEmailconfiguration { get; set; }
        public UpdateEmailconfiguration UpdateEmailconfiguration { get; set; }
    }

    public class InsertEmailconfiguration
    {
        //public int Id { get; set; }
        public String Display_Name { get; set; }
        public String Email_Address { get; set; }
        public String MailServer_SMTP { get; set; }
        public int SMTP_Port { get; set; }
        public int Server_Timeout { get; set; }
        public bool SMTP_Required_Authentication { get; set; }
        public bool Required_Squired_Password_Authentication { get; set; }
        public string User_Name { get; set; }
        public String Password { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateEmailconfiguration
    {
        public int Id { get; set; }
        public String Display_Name { get; set; }
        public String Email_Address { get; set; }
        public String MailServer_SMTP { get; set; }
        public int SMTP_Port { get; set; }
        public int Server_Timeout { get; set; }
        public bool SMTP_Required_Authentication { get; set; }
        public bool Required_Squired_Password_Authentication { get; set; }
        public string User_Name { get; set; }
        public String Password { get; set; }
        public bool IsActive { get; set; }
    }
}