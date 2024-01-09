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
        public string Insert(IssueTrackingInformation IssueTrackingInformation)
        {
            // throw new NotImplementedException();
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@IssueTrackerId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = IssueTrackingInformation.InsertIssueTrackinginfo.ToDictionary();
            disc3.Remove("IssueTrackerId");
            var IssueTrackerId = ExecNonQueryProcWithOutSaveChanges("insert_T_IssueTrackerInformation", disc3, vIndentId);


            _unitofWork.SaveChanges();
            return IssueTrackerId;
        }

        public bool Update(IssueTrackingInformation IssueTrackingInformation)
        {
            //  throw new NotImplementedException();
            var disc3 = IssueTrackingInformation.UpdateIssueTrackinginfo.ToDictionary();
           
           ExecNonQueryProcWithOutSaveChanges("Update_T_IssueTrackerInformation", disc3);


            _unitofWork.SaveChanges();
            return true;
        }
    }
}
