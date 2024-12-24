using HIMS.Model.HomeDelivery;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class ConsentMasterParam
    {
        public SaveConsentMasterParam SaveConsentMasterParam { get; set; }
        public UpdateConsentMasterParam UpdateConsentMasterParam { get; set; }
        public CancelConsentMasterParam CancelConsentMasterParam { get; set; }
    }

    public class SaveConsentMasterParam
    {
        public long ConsentId { get; set; }
        public long DepartmentId { get; set; }
      
        public string ConsentName { get; set; }
        public string ConsentDesc { get; set; }
        public long IsActive { get; set; }
      
        public long CreatedBy { get; set; }




    }
    public class UpdateConsentMasterParam
    {
        public long ConsentId { get; set; }
        public long DepartmentId { get; set; }

        public string ConsentName { get; set; }
        public string ConsentDesc { get; set; }
        public long IsActive { get; set; }

      
        public long ModifiedBy { get; set; }

    }


    public class CancelConsentMasterParam
    {
        public long ConsentId { get; set; }
        public long IsActive { get; set; }
      
    }
}

    