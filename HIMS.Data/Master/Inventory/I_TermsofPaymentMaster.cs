using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_TermsofPaymentMaster
    {
        public bool Insert(TermsofPaymentMasterParams tomMasterParams);
        public bool Update(TermsofPaymentMasterParams tomMasterParams);
    }
}
