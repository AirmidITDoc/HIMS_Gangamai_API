using HIMS.Common.Utility;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public class R_DoseMaster:GenericRepository,I_DoseMaster
    {
        public R_DoseMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(DoseMasterParams doseMasterParams)
        {
            var disc = doseMasterParams.UpdateDoseMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_DoseMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(DoseMasterParams doseMasterParams)
        {
            var disc = doseMasterParams.InsertDoseMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_DoseMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
