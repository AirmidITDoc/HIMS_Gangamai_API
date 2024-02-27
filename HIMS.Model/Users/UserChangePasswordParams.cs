using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Users
{
    public class UserChangePasswordParams
    {
        public ChangePassword ChangePassword { get; set; }

        public  LoginInsert LoginInsert { get; set; }
        public  LoginUpdate LoginUpdate { get; set; }
    }

     public class ChangePassword
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginInsert
    {
        // public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long AddedBy { get; set; }
        public bool IsActive { get; set; }
        public long RoleId { get; set; }
       // public string Role { get; set; }
        public long StoreId { get; set; }
        public bool IsDoctorType { get; set; }
        public long DoctorID { get; set; }
        public bool IsPOVerify { get; set; }
        public bool IsGRNVerify { get; set; }
        //public bool IsCreditBillScroll { get; set; }
        //public bool IsPharBalClearnace { get; set; }
        public bool IsCollection { get; set; }
        public bool IsBedStatus { get; set; }
        public bool IsCurrentStk { get; set; }
        public bool IsPatientInfo { get; set; }
        public bool IsDateInterval { get; set; }
        public long IsDateIntervalDays { get; set; }
        public string MailId { get; set; }
        public string MailDomain { get; set; }
        // public bool LoginStatus { get; set; }
        public bool AddChargeIsDelete { get; set; }
        public bool IsIndentVerify { get; set; }
        public bool IsPOInchargeVerify { get; set; }
        // public bool IsRefDocEditOpt { get; set; }
        public bool IsInchIndVfy { get; set; }
    }


    public class LoginUpdate
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
      // public string Password { get; set; }
        public long AddedBy { get; set; }
        public bool IsActive { get; set; }
        public long RoleId { get; set; }
     //   public string Role { get; set; }
        public long StoreId { get; set; }
        public bool IsDoctorType { get; set; }
        public long DoctorId { get; set; }
        public bool IsPOVerify { get; set; }
        public bool IsGRNVerify { get; set; }
        //public bool IsCreditBillScroll { get; set; }
        //public bool IsPharBalClearnace { get; set; }
        public bool IsCollection { get; set; }
        public bool IsBedStatus { get; set; }
        public bool IsCurrentStk { get; set; }
        public bool IsPatientInfo { get; set; }
        public bool IsDateInterval { get; set; }
        public long IsDateIntervalDays { get; set; }
        public string MailId { get; set; }
        public string MailDomain { get; set; }
        // public bool LoginStatus { get; set; }
        public bool AddChargeIsDelete { get; set; }
        public bool IsIndentVerify { get; set; }
        public bool IsPOInchargeVerify { get; set; }
        // public bool IsRefDocEditOpt { get; set; }
        public bool IsInchIndVfy { get; set; }
    }
}
