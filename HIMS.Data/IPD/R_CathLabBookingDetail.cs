using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_CathLabBookingDetail : GenericRepository, I_CathLabBookingDetail
    {
        public R_CathLabBookingDetail(IUnitofWork unitofWork) : base(unitofWork)
        {

        }


        public string Insert(CathLabBookingDetailParams CathLabBookingDetailParams)
        {
            //  throw new NotImplementedException();


            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OTCathLabBokingID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc = CathLabBookingDetailParams.CathLabBookingDetailInsert.ToDictionary();
            disc.Remove("OTCathLabBokingID");
            var OTCathLabBokingID = ExecNonQueryProcWithOutSaveChanges("insert_T_OTCathLabBooking_1", disc, outputId1);

            _unitofWork.SaveChanges();
            return OTCathLabBokingID;
        }

        public bool Update(CathLabBookingDetailParams CathLabBookingDetailParams)
        {
            // throw new NotImplementedException();

            var disc = CathLabBookingDetailParams.CathLabBookingDetailUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_OTCathLabBooking_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }

   


}

