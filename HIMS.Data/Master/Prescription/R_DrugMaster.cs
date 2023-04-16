using HIMS.Common.Utility;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public class R_DrugMaster:GenericRepository,I_DrugMaster
    {
        public R_DrugMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(DrugMasterParams drugMasterParams)
        {
            var disc = drugMasterParams.UpdateDrugMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_DrugMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(DrugMasterParams drugMasterParams)
        {
            var disc = drugMasterParams.InsertDrugMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_DrugMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
