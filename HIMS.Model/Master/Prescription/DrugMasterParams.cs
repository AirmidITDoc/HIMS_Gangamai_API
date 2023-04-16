using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Prescription
{
    public class DrugMasterParams
    {
        public InsertDrugMaster InsertDrugMaster { get; set; }
        public UpdateDrugMaster UpdateDrugMaster { get; set; }
    }
    public class InsertDrugMaster 
    {
        public string DrugName { get; set; }
        public long GenericId { get; set; }
        public long ClassId { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
    
    }
    public class UpdateDrugMaster
    {
        public long DrugId { get; set; }
        public string DrugName { get; set; }
        public long GenericId { get; set; }
        public long ClassId { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }

    }
}
