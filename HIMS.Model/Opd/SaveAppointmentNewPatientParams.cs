using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
   public class SaveAppointmentNewPatientParams
    {
        public RegistrationInsert RegistrationInsert { get; set; }
        
        public VisitDetailsInsert VisitDetailsInsert { get; set; }
        public TokenNumberWithDoctorWiseInsert TokenNumberWithDoctorWiseInsert { get; set; }
        
    }

    public class RegistrationInsert
    {
        public long RegId { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime RegTime { get; set; }
        public long PrefixId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinNo { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Age { get; set; }
        public long GenderId { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public long AddedBy { get; set; }

        public String RegNo { get; set; }
        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public long MaritalStatusId { get; set; }
        public bool IsCharity { get; set; }
        public long ReligionId { get; set; }
        public long AreaId { get; set; }
        public long VillageId { get; set; }
        public long TalukaId { get; set; }
        public double PatientWeight { get; set; }
        public string AreaName { get; set; }
        public String Notes { get; set; }
        public string AadharCardNo { get; set; }
        public string PanCardNo { get; set; }
      
    }
    public class VisitDetailsInsert
    {
        public long VisitId { get; set; }
        public long RegID { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime VisitTime { get; set; }
        public long UnitId { get; set; }
        public long PatientTypeId { get; set; }
        public long ConsultantDocId { get; set; }
        public long RefDocId { get; set; }
        // public String OPDNo { get; set; }
        public long TariffId { get; set; }
        public long CompanyId { get; set; }
        public long AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public long ClassId { get; set; }
        public long DepartmentId { get; set; }
        public long PatientOldNew { get; set; }
        public Boolean FirstFollowupVisit { get; set; }
        public long AppPurposeId { get; set; }
        public DateTime FollowupDate { get; set; }

        public Boolean IsMark { get; set; }
        public Boolean IsXray { get; set; }

    }
    public class TokenNumberWithDoctorWiseInsert
    {
        public int PatVisitID { get; set; }
        
    }
    
}

