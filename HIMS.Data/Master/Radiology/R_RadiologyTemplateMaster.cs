using HIMS.Common.Utility;
using HIMS.Model.Master.Radiology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Radiology
{
    public class R_RadiologyTemplateMaster:GenericRepository,I_RadiologyTemplateMaster
    {
        public R_RadiologyTemplateMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(RadiologyTemplateMasterParams rtMasterParams)
        {
            var disc = rtMasterParams.UpdateRadiologyTemplateMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_Radiology_TemplateMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(RadiologyTemplateMasterParams rtMasterParams)
        {
            var disc = rtMasterParams.InsertRadiologyTemplateMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_Radiology_TemplateNameMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
