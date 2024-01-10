using HIMS.Common.Utility;
using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Inventory
{
    public class R_IssueTrackingInfo : GenericRepository, I_IssueTrackingInfo
    {

        public R_IssueTrackingInfo(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string Insert(IssueTrackerParams issueTrackerParams)
        {
            // throw new NotImplementedException();
            var vIssueTrackerId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@IssueTrackerId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = issueTrackerParams.InsertIssueTracker.ToDictionary();
            disc3.Remove("IssueTrackerId");
            var IssueTrackerId = ExecNonQueryProcWithOutSaveChanges("m_insert_T_IssuetrackerInformation", disc3, vIssueTrackerId);


            _unitofWork.SaveChanges();
            return IssueTrackerId;
        }

        public bool Update(IssueTrackerParams issueTrackerParams)
        {
           var disc3 = issueTrackerParams.UpdateIssueTracker.ToDictionary();
           ExecNonQueryProcWithOutSaveChanges("m_Update_T_IssuetrackerInformation", disc3);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool UpdateStatus(IssueTrackerParams issueTrackerParams)
        {
            var disc3 = issueTrackerParams.UpdateIssueTrackerStatus.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_T_IssuetrackerInformation", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
