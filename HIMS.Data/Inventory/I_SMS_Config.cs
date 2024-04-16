using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Inventory
{
    public  interface I_SMS_Config
    {
        public string InsertSMSConfig(SMS_ConfigParam SMS_ConfigParam);
        public bool UpdateSMSConfigParam(SMS_ConfigParam SMS_ConfigParam);
    }
}
