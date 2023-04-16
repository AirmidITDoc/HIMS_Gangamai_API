using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;


namespace HIMS.Data.Transaction
{
   public interface I_VendorAdvancePayment
    {
        bool Update(VendorAdvancePaymentParams vendorAdvancePaymentParams);
        bool Save(VendorAdvancePaymentParams vendorAdvancePaymentParams);
       // List<dynamic> getIssueTrackingList(VendorAdvancePaymentParams vendorAdvancePaymentParams);
    }
}
