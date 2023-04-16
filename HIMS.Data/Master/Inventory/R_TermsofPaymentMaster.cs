using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_TermsofPaymentMaster : GenericRepository, I_TermsofPaymentMaster
    {
        public R_TermsofPaymentMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(TermsofPaymentMasterParams todMasterParams)
        {
            var disc = todMasterParams.UpdateTermsofPaymentMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_TermsOfPaymentMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(TermsofPaymentMasterParams todMasterParams)
        {
            var disc = todMasterParams.InsertTermsofPaymentMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_TermsOfPaymentMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }

    }
}