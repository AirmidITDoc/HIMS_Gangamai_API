using HIMS.Common.Utility;
using HIMS.Model.IPD;
using HIMS.Model.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pathology
{
    public class R_PathologyTemplateResult:GenericRepository,I_PathologyTemplateResult
    {
        public R_PathologyTemplateResult(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(PathologyTemplateResultParams PathologyTemplateResultParams)
        {
            var disc3 = PathologyTemplateResultParams.DeletePathologyReportTemplateDetails.ToDictionary();
            var PathReportId = disc3["PathReportId"];
            ExecNonQueryProcWithOutSaveChanges("Delete_T_PathologyReportTemplateDetails", disc3);

            
            foreach (var a in PathologyTemplateResultParams.InsertPathologyReportTemplateDetails)
            {
                
                var disc1 = a.ToDictionary();
                disc1["PathReportId"] = (int)Convert.ToInt64(PathReportId);
                ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportTemplateDetails_1", disc1);
            }

            //PathologyTemplateResultParams.UpdatePathologyReportHeader.PathReportID = PathologyTemplateResultParams.
            var disc = PathologyTemplateResultParams.UpdatePathTemplateReportHeader.ToDictionary();
            disc["PathReportID"] = (int)Convert.ToInt64(PathReportId);
            ExecNonQueryProcWithOutSaveChanges("update_T_PathologyReportHeader_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}

   