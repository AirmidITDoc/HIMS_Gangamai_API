using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class MOTTableMasterParam
    {
        public SaveOTTableMasterParam SaveOTTableMasterParam { get; set; }
        public UpdateOTTableMasterParam UpdateOTTableMasterParam { get; set; }
        public CancelOTTableMasterParam CancelOTTableMasterParam { get; set; }
    }
    public class SaveOTTableMasterParam
    {
        public string OTTableName { get; set; }
        public long LocationId { get; set; }
        public long IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long OTTableId { get; set; }
    }
    public class UpdateOTTableMasterParam
    {
        public long OTTableId { get; set; }
        public string OTTableName { get; set; }
        public long LocationId { get; set; }
        public long IsActive { get; set; }
        public long ModifiedBy { get; set; }

    }
    public class CancelOTTableMasterParam
    { 
        public long OTTableId { get; set; }
        public long IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }



    }
}
