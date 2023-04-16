using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Prescription
{
    public class DoseMasterParams
    {
        public InsertDoseMaster InsertDoseMaster { get; set; }
        public UpdateDoseMaster UpdateDoseMaster { get; set; }
    }
    public class InsertDoseMaster
    {
        public string DoseName { get; set; }
        public string DoseNameInEnglish { get; set; }
        public string DoseNameInMarathi { get; set; }
        public float DoseQtyPerDay { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }

    public class UpdateDoseMaster
    {
        public long DoseId { get; set; }
        public string DoseName { get; set; }
        public string DoseNameInEnglish { get; set; }
        public string DoseNameInMarathi { get; set; }
        public float DoseQtyPerDay { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }

    }
}
