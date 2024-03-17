using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.HomeDelivery;

namespace HIMS.Data.HomeDelivery
{
    public class R_HomeDeliveryOrder : GenericRepository, I_HomeDeliveryOrder
    {
        public R_HomeDeliveryOrder(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string HomeDeliveryOrderInsert(HomeDeliveryOrderParams homeDeliveryOrderParams)
        {
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OrderId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = homeDeliveryOrderParams.HomeDeliveryOrderInsert.ToDictionary();
            disc3.Remove("OrderId");
            var IndentId = ExecNonQueryProcWithOutSaveChanges("m_insert_T_HomeDeliveryOrder_1", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return IndentId;
        }
    }
}
