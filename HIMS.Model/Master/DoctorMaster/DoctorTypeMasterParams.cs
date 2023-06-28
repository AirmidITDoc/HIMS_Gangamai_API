using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.DoctorMaster
{
   public class DoctorTypeMasterParams
    {

        public DoctortTypeMasterInsert DoctortTypeMasterInsert { get; set; }
        public DoctorTypeMasterUpdate DoctorTypeMasterUpdate { get; set; }

    }

    public class DoctortTypeMasterInsert
    {
        public String DoctorType { get; set; }
        public bool IsActive { get; set; }

    }

    public class DoctorTypeMasterUpdate
    {

        public int Id { get; set; }
        public String DoctorType { get; set; }
        public bool IsActive { get; set; }
       
    }
}

