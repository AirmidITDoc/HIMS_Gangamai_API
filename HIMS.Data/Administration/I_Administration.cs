using HIMS.Model.Administration;
using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Administration
{
    public  interface I_Administration
    {
        public bool UpdateBillcancellation(AdministrationParam administrationParam);
        public bool InsertDoctorShareMaster(DoctorShareParam doctorShareParam);
        public bool UpdateDoctorShareMaster(DoctorShareParam doctorShareParam);
        public bool DoctorShareProcess(DoctorShareProcessParam doctorShareProcessParam);
        public bool IPDischargeCancel(IPDischargeCancelParam iPDischargeCancelParam);

    }
}
