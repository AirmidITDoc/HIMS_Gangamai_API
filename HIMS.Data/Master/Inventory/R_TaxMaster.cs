using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_TaxMaster:GenericRepository,I_TaxMaster
    {
        public R_TaxMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(TaxMasterParams taxMasterParams)
        {
            var disc = taxMasterParams.UpdateTaxMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_TaxNatureMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(TaxMasterParams taxMasterParams)
        {
            var disc = taxMasterParams.InsertTaxMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_TaxNatureMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
