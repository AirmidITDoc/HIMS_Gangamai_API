using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public class R_PathologyTemplateMaster:GenericRepository,I_PathologyTemplateMaster
    {
        public R_PathologyTemplateMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(PathologyTemplateMasterParams pathTemplateMasterParams)
        {
            var disc = pathTemplateMasterParams.UpdatePathologyTemplateMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("M_Update_M_PathologyTemplateMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(PathologyTemplateMasterParams pathTemplateMasterParams)
        {
            var disc = pathTemplateMasterParams.InsertPathologyTemplateMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("M_Insert_M_PathologyTemplateMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
