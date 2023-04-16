using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_OTBookingDetail
    {
        public String Insert(OTBookingDetailParams OTBookingDetailParams);
        public bool Update(OTBookingDetailParams OTBookingDetailParams);
    }
}
