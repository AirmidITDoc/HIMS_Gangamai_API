using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.WhatsAppEmail
{
    public class WhatsappSmsparam
    {
        public InsertWhatsappsmsInfo InsertWhatsappsmsInfo { get; set; }
        public InsertEamil InsertEamil { get; set; }
    }

    public class InsertWhatsappsmsInfo
    {
        public string MobileNumber { get; set; }
        public string SMSString { get; set; }
        public bool IsSent { get; set; }
        public string SMSType { get; set; }
        public string SMSFlag { get; set; }
        public DateTime SMSDate { get; set; }
        public int TranNo { get; set; }
        public int PatientType { get; set; }
        public int TemplateId { get; set; }
        public string SMSurl { get; set; }
        public string FilePath { get; set; }
        public int SMSOutGoingID { get; set; }
    }

    public class InsertEamil
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public int Status { get; set; }
        public int Retry { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentLink { get; set; }
        public int Id { get; set; }
    }

}