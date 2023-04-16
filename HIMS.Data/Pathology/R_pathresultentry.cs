using HIMS.Common.Utility;
using HIMS.Model.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pathology
{
    public class R_pathresultentry :GenericRepository,I_pathresultentry
    {
        public R_pathresultentry(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(pathresultentryparam pathresultentryparam)
        {
            // throw new NotImplementedException();
            var disc1 = pathresultentryparam.Deletepathreportheader.ToDictionary();
            var PathReportId = disc1["PathReportID"];
            ExecNonQueryProcWithOutSaveChanges("Delete_T_PathologyReportDetails", disc1);
//for
            
          
            foreach (var a in pathresultentryparam.Insertpathreportdetail)
            {

                var disc2 = a.ToDictionary();
                disc2["PathReportId"] = (int)Convert.ToInt64(PathReportId);
                ExecNonQueryProcWithOutSaveChanges("insert_PathRrptDet_1", disc2);
            }


            var disc3 = pathresultentryparam.Updatepathreportheader.ToDictionary();
            disc3["PathReportID"] = (int)Convert.ToInt64(PathReportId);
            ExecNonQueryProcWithOutSaveChanges("update_T_PathologyReportHeader_1", disc3);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
