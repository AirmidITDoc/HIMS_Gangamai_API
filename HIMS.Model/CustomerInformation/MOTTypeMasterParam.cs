using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class MOTTypeMasterParam
    {
       public SaveMOTTypeMasterParam SaveMOTTypeMasterParam { get; set; }
       public UpdateOTTypemasterParam UpdateOTTypemasterParam { get; set; }
       public CancelOTTypeMasterParam CancelOTTypeMasterParam { get; set; }
    }
    public class SaveMOTTypeMasterParam
    {
        public string TypeName { get; set; }
        public long IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long OTTypeId { get; set; }
    }
    public class UpdateOTTypemasterParam
    {
        public long OTTypeId { get; set; }
        public string TypeName { get; set; }
        public long IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

    }
    public class CancelOTTypeMasterParam
    {
      public long OTTypeId { get; set; }
      public long IsCancelled { get; set;}
     public long IsCancelledBy { get; set; }

    }
    
}
