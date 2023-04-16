using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_OTRequest :GenericRepository,I_OTRequest
    {
        public R_OTRequest(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(OTRequestparam OTRequestparam)
        {
            // throw new NotImplementedException();

           
            var disc = OTRequestparam.OTTableRequestInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_OTBookingRequest_details", disc);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(OTRequestparam OTRequestparam)
        {
            // throw new NotImplementedException();
            var disc = OTRequestparam.OTTableRequestUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_OTCathLabBooking_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
