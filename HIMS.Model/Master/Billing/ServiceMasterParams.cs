using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
    public class ServiceMasterParams
    {
        public ServiceMasterInsert ServiceMasterInsert { get; set; }

        public ServiceMasterUpdate ServiceMasterUpdate { get; set; }
        public List<ServiceDetailInsert> ServiceDetailInsert { get; set; }
        public ServiceDetDelete ServiceDetDelete { get; set; }

    }

    public class ServiceMasterInsert
    {     
        public int GroupId { get; set; } 
        public String ServiceShortDesc { get; set; }
        public String ServiceName { get; set; }
        public float Price { get; set; }
        public Boolean IsEditable { get; set; }
        public Boolean CreditedtoDoctor { get; set; }
        public int IsPathology { get; set; }
        public int IsRadiology { get; set; }
        public Boolean IsDeleted { get; set; }
        public int PrintOrder { get; set; }
        public int IsPackage { get; set; }
        public int SubgroupId { get; set; }

        public int DoctorId { get; set; }
        public bool IsEmergency { get; set; }
        public int EmgAmt { get; set; }
        public float EmgPer { get; set; }
        public bool IsDocEditable { get; set; }
        public int ServiceId { get; set; }


    }

    public class ServiceDetailInsert
    {
        public int ServiceId { get; set; }
        public int TariffId { get; set; }
        public int ClassId { get; set; }
        public int ClassRate { get; set; }
        public DateTime EffectiveDate { get; set; }
        
    }

    public class ServiceDetDelete
    {
        public int ServiceId { get; set; }
        public int TariffId { get; set; }
        

    }

    public class ServiceMasterUpdate
    {
        public int GroupId { get; set; }
        public String ServiceShortDesc { get; set; }
        public String ServiceName { get; set; }
        public float Price { get; set; }
        public Boolean IsEditable { get; set; }
        public Boolean CreditedtoDoctor { get; set; }
        public int IsPathology { get; set; }
        public int IsRadiology { get; set; }
        public Boolean IsDeleted { get; set; }
        public int PrintOrder { get; set; }
        public int IsPackage { get; set; }
        public int SubgroupId { get; set; }

        public int DoctorId { get; set; }
        public bool IsEmergency { get; set; }
        public int EmgAmt { get; set; }
        public float EmgPer { get; set; }
        public bool IsDocEditable { get; set; }
        public int ServiceId { get; set; }


    }

}
