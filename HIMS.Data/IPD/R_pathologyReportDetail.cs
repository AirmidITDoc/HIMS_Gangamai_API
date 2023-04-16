using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_pathologyReportDetail :GenericRepository,I_pathologyReportDetail
    {
        public R_pathologyReportDetail(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }


        public bool Insert(pathologyReportDetailParams pathologyReportDetailParams)
        {
            //throw new NotImplementedException();

            var dic = pathologyReportDetailParams.DeletePathologyReportDetail.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Delete_T_PathologyReportHeader_1", dic);

            pathologyReportDetailParams.InsertPathologyReportDetail.PathReportId = Convert.ToInt32(pathologyReportDetailParams.DeletePathologyReportDetail.PathReportId);
            var dic1 = pathologyReportDetailParams.InsertPathologyReportDetail.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_PathologyReportDetails_1_New", dic1);

            pathologyReportDetailParams.UpdatePathologyReportHeader.PathReportID = Convert.ToInt32(pathologyReportDetailParams.InsertPathologyReportDetail.PathReportId);
            var disc2 = pathologyReportDetailParams.UpdatePathologyReportHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_T_PathologyReportHeader", disc2);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(pathologyReportDetailParams pathologyReportDetailParams)
        {
           

            _unitofWork.SaveChanges();
            return true;
        }

    }
}
