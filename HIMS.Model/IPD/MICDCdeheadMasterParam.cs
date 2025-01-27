using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class MICDCdeheadMasterParam
    {
        public SaveMICDCdeheadMasterParam SaveMICDCdeheadMasterParam { get; set; }
        public UpdateMICDCdeheadMasterParam UpdateMICDCdeheadMasterParam { get; set; }
    }
    public class SaveMICDCdeheadMasterParam
    { 
        public int ICDCdeMId { get; set; }
        public string ICDCodeName { get; set; }
        public long IsActive { get; set; }
        
        public int CreatedBy { get; set; }

       


    }
    public class UpdateMICDCdeheadMasterParam
    {
        public int ICDCdeMId { get; set; }
        public string ICDCodeName { get; set; }
       
        public int IsActive { get; set; }
        public long ModifiedBy { get; set; }
       
    }
}
