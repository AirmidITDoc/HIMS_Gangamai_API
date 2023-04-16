using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class RegisteredPatientAdmissionParams
    {
      
        public AdmissionInsert AdmissionInsert { get; set; }
    }
   /* public class BedUpdate
    {
        public int BedId { get; set; }
    }*/

  
    public class AdmissionInsert
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
}
