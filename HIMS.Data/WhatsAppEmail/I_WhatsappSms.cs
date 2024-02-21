using HIMS.Model.WhatsAppEmail;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.WhatsAppEmail
{
    public interface I_WhatsappSms
    {
        public string Insert(WhatsappSmsparam WhatsappSmsparam);
        public string InsertEmail(WhatsappSmsparam WhatsappSmsparam);
    }
}
