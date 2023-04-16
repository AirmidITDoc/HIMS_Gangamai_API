namespace HIMS.Model.Opd
{
    public class Login
    {
        //public string UserName { get; set; }
        //public string Password { get; set; }
        //public long UserId { get; set; }
        public string Role { get; set; }
        //public string Email { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long AddedBy { get; set; }
        public bool IsActive { get; set; }
        public long RoleId { get; set; }
        public long StoreId { get; set; }
        public bool IsDoctorType { get; set; }
        public long DoctorID { get; set; }
        public bool IsPOVerify { get; set; }
        public bool IsGRNVerify { get; set; }
        public bool IsCreditBillScroll { get; set; }
        public bool IsPharBalClearnace { get; set; }
        public bool IsCollection { get; set; }
        public bool IsBedStatus { get; set; }
        public bool IsCurrentStk { get; set; }
        public bool IsPatientInfo { get; set; }
        public bool IsDateInterval { get; set; }
        public bool IsDateIntervalDays { get; set; }
        public string MailId { get; set; }
        public string MailDomain { get; set; }
        public bool LoginStatus { get; set; }
        public bool AddChargeIsDelete { get; set; }
        public bool IsIndentVerify { get; set; }
    }

}
