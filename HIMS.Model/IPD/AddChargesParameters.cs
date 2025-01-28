using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class AddChargesParameters
    {

        public AddChargesParameter SaveAddChargesParameters { get; set; }
       
        public List<ChargesIPPackage> ChargesIPPackageInsert { get; set; }
    }

    public class AddChargesParameter
    { 
        public long ChargeID { get; set; }
        public DateTime ChargesDate { get; set; }
        public long OPD_IPD_Type { get; set; }
        public long OPD_IPD_Id { get; set; }
        public long ServiceId { get; set; }
        public Double Price { get; set; }
        public float Qty { get; set; }
        public Double TotalAmt { get; set; }
        public Double ConcessionPercentage { get; set; }
        public Double ConcessionAmount { get; set; }
        public Double NetAmount { get; set; }
        public long DoctorId { get; set; }
        public Double DocPercentage { get; set; }
        public Double DocAmt { get; set; }
        public Double HospitalAmt { get; set; }
        public bool IsGenerated { get; set; }
        public long AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public bool IsPathology { get; set; }
        public bool IsRadiology { get; set; }
        public bool IsPackage { get; set; }
        public long PackageMainChargeID { get; set; }
        public bool IsSelfOrCompanyService { get; set; }
        public long PackageId { get; set; }
        public DateTime ChargeTime { get; set; }
        public long ClassId { get; set; }
    }

   
    public class ChargesIPPackage
    {
        
        public DateTime ChargesDate { get; set; }
        public bool OPD_IPD_Type { get; set; }
        public int OPD_IPD_Id { get; set; }
        public int ServiceId { get; set; }
        public long Price { get; set; }
        public long Qty { get; set; }
        public float TotalAmt { get; set; }
        public float ConcessionPercentage { get; set; }
        public float ConcessionAmount { get; set; }
        public float NetAmount { get; set; }
        public int DoctorId { get; set; }
        public float DocPercentage { get; set; }
        public float DocAmt { get; set; }
        public float HospitalAmt { get; set; }
        public bool IsGenerated { get; set; }
        public int AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public bool IsPathology { get; set; }
        public bool IsRadiology { get; set; }
        public bool IsPackage { get; set; }
        public bool IsSelfOrCompanyService { get; set; }
        public int PackageId { get; set; }
        public int PackageMainChargeID { get; set; }
        public DateTime ChargeTime { get; set; }

    }
}
