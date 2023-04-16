using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_OTEndoscopy : GenericRepository, I_OTEndoscopy
    {
        public R_OTEndoscopy(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

     
        public string Insert(OTEndoscopyParam OTEndoscopyParam)
        {
            // throw new NotImplementedException();
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OTEndoscopyBookingID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc = OTEndoscopyParam.OTEndoscopyInsert.ToDictionary();
            disc.Remove("OTEndoscopyBookingID");
            var OtBookingId = ExecNonQueryProcWithOutSaveChanges("insert_T_EndoscopyBooking_1", disc, outputId1);

            _unitofWork.SaveChanges();
            return OtBookingId;
        }

        public bool Update(OTEndoscopyParam OTEndoscopyParam)
        {
            // throw new NotImplementedException();

            var disc = OTEndoscopyParam.OTEndoscopyUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_EndoscopyBooking_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
