using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IP_Charges_Delete:GenericRepository,I_IP_Charges_Delete
    {
        public R_IP_Charges_Delete(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Update(IP_Charges_DeleteParams IP_Charges_DeleteParams)
        {
            //var G_ChargesId = IP_Charges_DeleteParams.UpdateIP_Charge_Delete.ToDictionary();
            //var G_UserId = IP_Charges_DeleteParams.UpdateIP_Charge_Delete.ToDictionary();

            var disc1= IP_Charges_DeleteParams.UpdateIP_Charge_Delete.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_delete_Addcharges",disc1);

          
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
