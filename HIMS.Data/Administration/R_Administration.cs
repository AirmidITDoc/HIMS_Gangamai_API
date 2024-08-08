using HIMS.Data.CustomerPayment;
using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Model.Inventory;
using HIMS.Common.Utility;
using HIMS.Model.Administration;

namespace HIMS.Data.Administration
{
    public  class R_Administration : GenericRepository, I_Administration
    {
        public R_Administration(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool UpdateBillcancellation(AdministrationParam administrationParam)
        {
            if (administrationParam.BillCancellationParam.OP_IP_type == 0 )
            {
                var disc3 = administrationParam.BillCancellationParam.ToDictionary();
                disc3.Remove("OP_IP_type");
                ExecNonQueryProcWithOutSaveChanges("OP_BILL_CANCELLATION", disc3);
            }
            else
            {
                var disc3 = administrationParam.BillCancellationParam.ToDictionary();
                disc3.Remove("OP_IP_type");
                ExecNonQueryProcWithOutSaveChanges("IP_BILL_CANCELLATION", disc3);
            }

            _unitofWork.SaveChanges();
            return true;
        }


    }
}


    

