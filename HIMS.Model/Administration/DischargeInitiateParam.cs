using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class DischargeInitiateParam
    {
        public List<SaveDischargeInitiateParam> SaveDischargeInitiateParam { get; set; }
        public UpdateDischargeInitiateParam UpdateDischargeInitiateParam { get; set; }
    }
    public class SaveDischargeInitiateParam
    {
       
        public long AdmID { get; set; }
        public string DepartmentName { get; set; }
        public long DepartmentID { get; set; }
        public long IsApproved { get; set; }
        public long ApprovedBy { get; set; }
        public DateTime ApprovedDatetime { get; set; }
      

    }

    public class UpdateDischargeInitiateParam
    {
        public long AdmID { get; set; }

        public long IsInitinatedDischarge { get; set; }
      
    }
}
