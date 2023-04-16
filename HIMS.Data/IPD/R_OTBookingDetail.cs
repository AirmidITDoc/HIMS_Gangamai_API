using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_OTBookingDetail :GenericRepository,I_OTBookingDetail
    {
        public R_OTBookingDetail(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string Insert(OTBookingDetailParams OTBookingDetailParams)
        {
            // throw new NotImplementedException();


            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OTBookingID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc = OTBookingDetailParams.OTTableBookingDetailInsert.ToDictionary();
            disc.Remove("OTBookingID");
            var OtBookingId = ExecNonQueryProcWithOutSaveChanges("insert_T_OTBooking_1", disc,outputId1);

            _unitofWork.SaveChanges();
            return OtBookingId;
        }

        public bool Update(OTBookingDetailParams OTBookingDetailParams)
        {
           //
            var disc = OTBookingDetailParams.OTTableBookingDetailUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_OTBooking_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
