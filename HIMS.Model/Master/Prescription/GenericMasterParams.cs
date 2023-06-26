using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Prescription
{
    public class GenericMasterParams
    {
        public InsertGenericMaster InsertGenericMaster { get; set; }
        public UpdateGenericMaster UpdateGenericMaster { get; set; }
    }
    public class InsertGenericMaster 
    {
        public string GenericName { get; set; }
        public bool IsActive { get; set; }
      
    }
    public class UpdateGenericMaster 
    {
        public long GenericId { get; set; }
        public string GenericName { get; set; }
        public bool IsActive { get; set; }
       
    }
}
