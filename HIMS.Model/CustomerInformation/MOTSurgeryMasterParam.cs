using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class MOTSurgeryMasterParam
    {
        public SaveMOTSurgeryMasterParam SaveMOTSurgeryMasterParam { get; set; }
        public UpdateMOTSurgeryMasterParam UpdateMOTSurgeryMasterParam { get; set; }
        public CancelMOTSurgeryMasterParam CancelMOTSurgeryMasterParam { get; set; }
    }
    public class SaveMOTSurgeryMasterParam
    { 
        public long surgeryCategoryId { get; set; }
        public string SurgeryName { get; set; }
        public long DepartmentId { get; set; }
        public long SurgeryAmount { get; set; }
        public long SiteDescId { get; set; }
        public long OTTemplateID { get; set; }
        public long ServiceId { get; set; }
        public long IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long SurgeryId { get; set; }

    }
    public class UpdateMOTSurgeryMasterParam
    {
        public long SurgeryId { get; set; }
        public long surgeryCategoryId { get; set; }
        public string SurgeryName { get; set; }
        public long DepartmentId { get; set; }
        public long SurgeryAmount { get; set; }
        public long SiteDescId { get; set; }
        public long OTTemplateID { get; set; }
        public long ServiceId { get; set; }
        public long IsActive { get; set; }
        public long ModifiedBy { get; set; }
    }
    public class CancelMOTSurgeryMasterParam
    { 
        public long SurgeryId { get; set; }
        public long IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }

    }
}
