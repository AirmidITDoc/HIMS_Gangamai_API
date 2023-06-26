using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class PatientTypeMasterParams
    {
        public PatientTypeMasterInsert PatientTypeMasterInsert { get; set; }
        public PatientTypeMasterUpdate PatientTypeMasterUpdate { get; set; }

    }

    public class PatientTypeMasterInsert
    {
        public String PatientType { get; set; }
        public bool IsActive { get; set; }

       
    }

    public class PatientTypeMasterUpdate
    {

        public int PatientTypeID { get; set; }
        public String PatientType { get; set; }
        public bool IsActive { get; set; }
      
    }
}

