using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master
{
    public class HospitalMaster
    {
        public long HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
    }
}
