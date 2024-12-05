using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class UptDocMergeParam
    {
        public   List<UptDocMergeParams> UptdateDocMergeParams { get; set; }
      
    }
    public class UptDocMergeParams
    {
      
        public long DoctorId { get; set; }
       
        public long DocDupId { get; set; }

    }

   
}
