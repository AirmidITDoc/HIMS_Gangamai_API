using HIMS.Model.Master;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
    public interface I_CustomerEnquiry
    {
        bool Update(CustomerEnquiryParams customerEnquiryParams);
        bool Save(CustomerEnquiryParams customerEnquiryParams);
    }
}
