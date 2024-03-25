using HIMS.Data.HomeDelivery;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.HomeDelivery;
using System.IO;

namespace HIMS.Data.HomeDelivery
{
    public class R_HomeDeliveryLogin : GenericRepository, I_HomeDeliveryLogin
    {

        public R_HomeDeliveryLogin(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string HomeDeliveryLoginInsert(HomeDeliveryLoginParams homeDeliveryLoginParams)
        {
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@CustomerId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = homeDeliveryLoginParams.HomeDeliveryLoginCreate.ToDictionary();
            disc3.Remove("CustomerId");
            var IndentId = ExecNonQueryProcWithOutSaveChanges("m_insert_SS_MobileAppLogin_1", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return IndentId;
        }
        public bool HomeDeliveryProfileUpdate(HomeDeliveryLoginParams homeDeliveryLoginParams)
        {
            var disc3 = homeDeliveryLoginParams.HomeDeliveryLoginCreate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_SS_MobileAppProfileUpdate_1", disc3);

            _unitofWork.SaveChanges();
            return true;
        }

    }
}
