using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public class R_ParameterMasterAgeWise:GenericRepository,I_ParameterMasterAgeWise
    {
        public R_ParameterMasterAgeWise(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }
         public bool Update(ParameterMasterAgeWiseParams paraMasterAgeParams)
        {
            var disc = paraMasterAgeParams.UpdateParameterMasterAgeWise.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ParameterRangeMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(ParameterMasterAgeWiseParams paraMasterAgeParams)
        {
            var disc = paraMasterAgeParams.InsertParameterMasterAgeWise.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ParameterRangeWithAgeMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        } 
    }
}
