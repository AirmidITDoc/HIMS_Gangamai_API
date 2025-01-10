using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class TConsentInformationparams

    {
        public SaveTConsentInformationparams SaveTConsentInformationparams { get; set; }
        public UpdateTConsentInformationparams UpdateTConsentInformationparams { get; set; }

    }

    public class SaveTConsentInformationparams
    {
        public long ConsentId { get; set; }
        public DateTime ConsentDate { get; set; }

        public DateTime ConsentTime { get; set; }
        public long OPIPID { get; set; }
        public long OPIPType { get; set; }
        public long ConsentDeptId { get; set; }
        public long ConsentTempId { get; set; }
        public string ConsentName { get; set; }
        public string ConsentText { get; set; }

        public long CreatedBy { get; set; }



    }
    public class UpdateTConsentInformationparams
    {
        public long ConsentId { get; set; }
        public DateTime ConsentDate { get; set; }

        public DateTime ConsentTime { get; set; }
        public long OPIPID { get; set; }
        public long OPIPType { get; set; }
        public long ConsentDeptId { get; set; }
        public long ConsentTempId { get; set; }
        public string ConsentName { get; set; }
        public string ConsentText { get; set; }

        public long ModifiedBy { get; set; }


    }



}