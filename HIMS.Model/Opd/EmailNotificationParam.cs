using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
   public class EmailNotificationParam
    {
        public EmailInsert EmailInsert { get; set; }
    }

    public class EmailInsert
{
        public int Id { get; set; }
        public int NotificationType { get; set; }
        public DateTime SendDate { get; set; }
        public String ToAddress { get; set; }
        public String Subject { get; set; }
        public String EmailBody { get; set; }
        public String EmailCC { get; set; }
        public int IsSendMail { get; set; }
      
        public string AttachmentPath { get; set; }
    }
}
