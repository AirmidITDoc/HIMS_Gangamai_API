using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class AdmissionParams
    {
        public RegInsert RegInsert { get; set; }

        public AdmissionNewInsert AdmissionNewInsert { get; set; }

        //     public IpSMSTemplateInsert IpSMSTemplateInsert { get; set; }
        public AdmissionNewUpdate AdmissionNewUpdate { get; set; }


    }
    public class BedUpdate
    {
        public int BedId { get; set; }
    }

    public class RegInsert
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
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public long GenderID { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }

        public long AddedBy { get; set; }
         public long UpdatedBy { get; set; }

        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public long MaritalStatusId { get; set; }
        public bool IsCharity { get; set; }
        //  public string RegPrefix { get; set; }
        public long ReligionId { get; set; }
        public long AreaId { get; set; }
        //  public long VillageId { get; set; }
        public bool IsSeniorCitizen { get; set; }

        // public int VillageId { get; set; }
        // public int TalukaId { get; set; }
        // public float PatientWeight { get; set; }
        //  public string AreaName { get; set; }
         public string AadharCardNo { get; set; }
         public string PanCardNo { get; set; }
    }
    //public class RegistrationUpdate
    //{
    //    public int RegId { get; set; }
    //    //public DateTime RegDate { get; set; }
    //    //public DateTime RegTime { get; set; }
    //    public int PrefixId { get; set; }
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public string LastName { get; set; }
    //    public string Address { get; set; }
    //    public string City { get; set; }
    //    public string PinNo { get; set; }
    //    public DateTime DateOfBirth { get; set; }
    //    public String Age { get; set; }
    //    public int GenderID { get; set; }
    //    public string PhoneNo { get; set; }
    //    public string MobileNo { get; set; }

    //    public int UpdatedBy { get; set; }
    //    public string AgeYear { get; set; }
    //    public string AgeMonth { get; set; }
    //    public string AgeDay { get; set; }
    //    public int CountryId { get; set; }
    //    public int StateId { get; set; }
    //    public int CityId { get; set; }
    //    public int MaritalStatusId { get; set; }
    //    public bool IsCharity { get; set; }
    //    //public int ReligionId { get; set; }
    //    //public int AreaId { get; set; }
    //    //public int TalukaId { get; set; }
    //    //public int VillageID { get; set; }
    //    //public float PatientWeight { get; set; }
    //    //public string AreaName { get; set; }
    //    //public string AadharCardNo { get; set; }
    //    //public string PanCardNo { get; set; }
    //}
    public class AdmissionNewInsert
    {
        public int AdmissionID { get; set; }
        public int RegId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime AdmissionTime { get; set; }
        public int PatientTypeId { get; set; }
        public int HospitalID { get; set; }
        public int DocNameId { get; set; }
        public int RefDocNameId { get; set; }
        public int WardID { get; set; }
        public int Bedid { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime DischargeTime { get; set; }
        public int IsDischarged { get; set; }
        public int IsBillGenerated { get; set; }
        // public string IPDNo { get; set; }
        //public int IsCancelled { get; set; }
        public int CompanyId { get; set; }
        public int TariffId { get; set; }
        public int ClassId { get; set; }
        public int DepartmentId { get; set; }
        public string RelativeName { get; set; }
        public string RelativeAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public int RelationshipId { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsMLC { get; set; }
        public string MotherName { get; set; }
        public int AdmittedDoctor1 { get; set; }
        public int AdmittedDoctor2 { get; set; }
        public int RefByTypeId { get; set; }
        public int RefByName { get; set; }
        public int SubTpaComId { get; set; }
        public int PolicyNo { get; set; }
        public float AprovAmount { get; set; }
        public DateTime CompDOD { get; set; }
        //  public int IsPharClearUserId { get; set; }
        //  public DateTime PharDateTime { get; set; }
        // public string Comments { get; set; }
        //public Boolean IsReimbursement { get; set; }
    }
  
    public class AdmissionNewUpdate
    {
        public int AdmissionID { get; set; }

        public DateTime AdmissionDate { get; set; }
        public DateTime AdmissionTime { get; set; }
        public int PatientTypeID { get; set; }

        public int HospitalId { get; set; }
        public int CompanyId { get; set; }
        public int TariffId { get; set; }
        public int DepartmentId { get; set; }
        public int AdmittedNameID { get; set; }
        public String RelativeName { get; set; }
        public String RelativeAddress { get; set; }
        public String RelativePhoneNo { get; set; }
        public int RelationshipId { get; set; }
        public bool IsMLC { get; set; }
        public String MotherName { get; set; }
       
        public int AdmittedDoctor1 { get; set; }
        public int AdmittedDoctor2 { get; set; }
        public int RefByTypeId { get; set; }
        public int RefByName { get; set; }
        public int isUpdatedBy { get; set; }

        public int SubTpaComId { get; set; }
    }
    public class IpSMSTemplateInsert
    {
        public String VstCode { get; set; }
        public int MsgId { get; set; }
        public int PatientType { get; set; }
    }
}
