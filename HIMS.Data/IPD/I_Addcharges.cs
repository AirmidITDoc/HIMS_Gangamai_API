using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_Addcharges
    {
        bool InsertIPDPackageBill(AddChargesParameters AddChargesParameters);
        bool UpdateIPDPackageBill(AddChargesPara AddChargesPara);
        bool Save(AddChargesParams addChargesParams);
        bool delete(AddChargesParams addChargesParams);
        bool LabRequestSave(LabRequesChargesParams labRequesChargesParams);
    }
}
