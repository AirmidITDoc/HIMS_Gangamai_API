using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class GSTReCalculProcessParam
    {
        public InsertGSTReCalculProcessParam InsertGSTReCalculProcessParam { get; set; }
       
    }
   

    public class InsertGSTReCalculProcessParam
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
