using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class WhatsappSmsparam
    {
        public InsertWhatsappsmsInfo InsertWhatsappsmsInfo { get; set; }
    }

    public class InsertWhatsappsmsInfo
    {
        public String MobileNumber { get; set; }
        public String SMSString { get; set; }
        public bool IsSent { get; set; }
        public String SMSType { get; set; }
        public String SMSFlag { get; set; }
        public DateTime SMSDate { get; set; }
        public int TranNo { get; set; }
        public int TemplateId { get; set; }
        public string SMSurl { get; set; }
        public string FilePath { get; set; }
        public int SMSOutGoingID { get; set; }
    }


}