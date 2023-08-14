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
            ExecNonQueryProcWithOutSaveChanges("update_DrugMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(DrugMasterParams drugMasterParams)
        {
            var disc = drugMasterParams.InsertDrugMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_DrugMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
