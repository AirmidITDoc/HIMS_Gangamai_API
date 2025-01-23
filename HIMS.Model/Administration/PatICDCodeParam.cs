using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.Model.Administration
{
    public class PatICDCodeParam
    {
        public InsertPatICDCodeParamHeader InsertPatICDCodeParamHeader { get; set; }
        public UpdatePatICDCodeParamHeader UpdatePatICDCodeParamHeader { get; set; }
        public InsertPatICDCodeParamDetails InsertPatICDCodeParamDetails { get; set; }
        public UpdatePatICDCodeParamDetails UpdatePatICDCodeParamDetails { get; set; }
    }
    public class InsertPatICDCodeParamHeader
    {
        public int PatICDCodeId { get; set; }
        public DateTime ReqDate { get; set; }
        public DateTime ReqTime { get; set; }
        public int OP_IP_Type { get; set; }
        public int OP_IP_ID { get; set; }
        public int CreatedBy { get; set; }

       

    }

    public class UpdatePatICDCodeParamHeader
    {
        public int PatICDCodeId { get; set; }
        public DateTime ReqDate { get; set; }
        public DateTime ReqTime { get; set; }
        public int OP_IP_Type { get; set; }
        public int OP_IP_ID { get; set; }
        public int ModifiedBy { get; set; }


    }
    public class InsertPatICDCodeParamDetails
    {
        public int DId { get; set; }
        public int HId { get; set; }
        public string IcdCode { get; set; }
        public string IcdCodeDesc { get; set; }
        public string ICDCdeMainName { get; set; }
        public int MainICDCdeId { get; set; }
        public int CreatedBy { get; set; }
       


    }

    public class UpdatePatICDCodeParamDetails
    {
        public int DId { get; set; }
        public int HId { get; set; }
        public string IcdCode { get; set; }
        public string IcdCodeDesc { get; set; }
        public string ICDCdeMainName { get; set; }
        public int MainICDCdeId { get; set; }
        public int ModifiedBy { get; set; }
       

    }
}
