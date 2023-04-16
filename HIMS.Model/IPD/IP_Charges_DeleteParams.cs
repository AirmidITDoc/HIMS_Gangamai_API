using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class IP_Charges_DeleteParams
    {
        public UpdateIP_Charge_Delete UpdateIP_Charge_Delete { get; set; }
    }

    public class UpdateIP_Charge_Delete
    {
        public int G_ChargesId { get; set; }
        public int G_UserId { get; set; }
    }
}
