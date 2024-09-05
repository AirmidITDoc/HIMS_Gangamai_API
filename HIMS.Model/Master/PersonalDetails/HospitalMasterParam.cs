using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class HospitalMasterParam
    {
        
            public HospitalMasterInsert HospitalMasterInsert { get; set; }
            public HospitalMasterUpdate HospitalMasterUpdate { get; set; }

        }

        public class HospitalMasterInsert
    {
       // public long HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

    }

    public class HospitalMasterUpdate
    {

        public long HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

    }
}

