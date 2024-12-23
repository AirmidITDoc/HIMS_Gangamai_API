using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class CustomerAmcParams
    {
        public AmcSaveParams AmcSaveParams { get; set; }
        public CustomerAmcUpdate CustomerAmcUpdate { get; set; }
        public CustomerAmcCancel CustomerAmcCancel { get; set; }
    }
    public class AmcSaveParams
    {
        public long CustomerId { get; set; }
        public long AMCDuration { get; set; }
        public long AMCAmount { get; set; }
        public long CreatedBy { get; set; }
    }
    public class CustomerAmcUpdate
    {
        public long AmcId { get; set; }
        public long CustomerId { get; set; }
        public DateTime AMCStartDate { get; set; }
        public DateTime AMCEndDate { get; set; }
        public long AMCDuration { get; set; }
        public long AMCAmount { get; set; }
        public string Comments { get; set; }
        public long ModifiedBy { get; set; }

    }

    public class CustomerAmcCancel
    {
        public long AmcId { get; set; }
        public long IsCancelledBy { get; set; }

    }


}

