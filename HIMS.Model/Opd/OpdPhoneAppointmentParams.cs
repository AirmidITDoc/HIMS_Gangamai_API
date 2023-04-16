using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class OpdPhoneAppointmentParams
    {
        public OpdInsertPhoneAppointment OpdInsertPhoneAppointment { get; set; }
        public OpdCancelPhoneAppointment OpdCancelPhoneAppointment { get; set; }
    }
    public class OpdInsertPhoneAppointment
    {
        public DateTime AppDate { get; set; }
        public DateTime AppTime { get; set; }

        public int PhoneAppId { get; set; }
        public string SeqNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }                
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public DateTime PhAppDate { get; set; }
        public DateTime PhAppTime { get; set; }
        public long DepartmentId { get; set; }
        public long DoctorId { get; set; }
        public long AddedBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public string RegNo { get; set; }
    }

    public class OpdCancelPhoneAppointment
    {
        public long PhoneAppId { get; set; }
        public long IsCancelledBy { get; set; }

        public DateTime IsCancelledDate { get; set; }
    }
}
