using HIMS.Common.Utility;
using HIMS.Model.IPD;
using HIMS.Model.Radiology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Radiology
{
    public class R_RadiologyTemplateResult:GenericRepository,I_RadiologyTemplateResult
    {
        public R_RadiologyTemplateResult(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Update(RadiologyTemplateResultParams RadiologyTemplateResultParams)
        {

            var RadReportId = RadiologyTemplateResultParams.RadiologyReportHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_T_RadiologyReportHeader_1", RadReportId);


            _unitofWork.SaveChanges();
            return true;
        }
    }
}
