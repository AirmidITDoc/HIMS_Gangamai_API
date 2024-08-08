using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace HIMS.Model.Administration
{
    public class AdministrationParam
    {
        public BillCancellationParam BillCancellationParam { get; set; }
    }
    public class BillCancellationParam
    {
        public long OP_IP_type { get; set; }
        public long BillNo { get; set; }
    }
}
