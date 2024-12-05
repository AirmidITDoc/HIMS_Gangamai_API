using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class DischargeInitiateApprovalParam
    {
       
        public UpdateDischargeInitiateApprovalParam UpdateDischargeInitiateApprovalParam { get; set; }
    }
    public class UpdateDischargeInitiateApprovalParam
    {
        public long AdmId { get; set; }
      
        public long DepartmentID { get; set; }
        public long InitateDiscId { get; set; }
        public long IsApproved { get; set; }
        public long ApprovedBy { get; set; }
        public DateTime ApprovedDatetime { get; set; }
        public long IsNoDues { get; set; }
        public string Comments { get; set; }
      

    }

  
}
