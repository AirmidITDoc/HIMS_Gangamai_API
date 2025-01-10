using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class MOTSiteDescriptionMasterParam
    {
        public SaveMOTSiteDescriptionMasterParam SaveMOTSiteDescriptionMasterParam { get; set; }
        public UpdateMOTSiteDescriptionMasterParam UpdateMOTSiteDescriptionMasterParam { get; set; }
        public CancelMOTSiteDescriptionMasterParam CancelMOTSiteDescriptionMasterParam { get; set; }


    }
    public class SaveMOTSiteDescriptionMasterParam
    { 
        public string SiteDescriptionName { get; set; }
        public long SurgeryCategoryID { get; set; }
        public long IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long SiteDescId { get; set; }
    }
    public class UpdateMOTSiteDescriptionMasterParam
    {
        public long SiteDescId { get; set; }
        public string SiteDescriptionName { get; set; }
        public long SurgeryCategoryID { get; set; }
        public long IsActive { get; set; }
        public long ModifiedBy { get; set; }
    }
    public class CancelMOTSiteDescriptionMasterParam
    { 
        public long SiteDescId { get; set; }
        public long IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
    }
}
