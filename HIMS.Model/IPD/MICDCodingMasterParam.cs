using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class MICDCodingMasterParam
    {
        public SaveMICDCodingMasterParam SaveMICDCodingMasterParam { get; set; }
        public UpdateMICDCodingMasterParam UpdateMICDCodingMasterParam { get; set; }
    }
    public class SaveMICDCodingMasterParam
    { 
        public string ICDCode { get; set; }
        public string ICDCodeName { get; set; }
        public long MainICDCdeId { get; set; }
        public long IsActive { get; set; }
        public int CreatedBy { get; set; }
        public long ICDCodingId { get; set; }
    }
    public class UpdateMICDCodingMasterParam
    {
        public long ICDCodingId { get; set; }
        public string ICDCode { get; set; }
        public string ICDCodeName { get; set; }
        public long MainICDCdeId { get; set; }
        public int IsActive { get; set; }
        public long ModifiedBy { get; set; }
    }
}
