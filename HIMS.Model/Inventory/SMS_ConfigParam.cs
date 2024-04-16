using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class SMS_ConfigParam
    {
        public InsertSMSConfigParam InsertSMSConfigParam {  get; set; }
        public UpdateSMSConfigParam updateSMSConfigParam { get; set; }
    }
    public class InsertSMSConfigParam
    {
        public string Url { get; set; }
        public string keys { get; set; }
        public string campaign { get; set; }
        public long routeid { get; set; }
        public string SenderId { get; set; }
        public string Username { get; set; }
        public string Spassword { get; set; }
        public string StorageLocLink { get; set; }
        public string ConType { get; set; }
        public int ConfigId { get; set; }
        
    }
    public class UpdateSMSConfigParam
    {
        public long Configid {  get; set; }
        public string Url { get; set; }
        public string keys { get; set; }
        public string campaign { get; set; }
        public long routeid { get; set; }
        public string  SenderId { get; set; } 
        public string Username { get; set; } 
        public string Spassword { get; set; } 
        public string StorageLocLink { get; set; } 
         public string ConType { get; set; }
    }
}
