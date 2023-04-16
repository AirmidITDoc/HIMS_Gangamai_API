using HIMS.Common.Utility;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public class R_GenericMaster:GenericRepository,I_GenericMaster
    {
        public R_GenericMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(GenericMasterParams genericMasterParams)
        {
            var disc = genericMasterParams.UpdateGenericMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_GenericMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(GenericMasterParams genericMasterParams)
        {
            var disc = genericMasterParams.InsertGenericMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_GenericMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
