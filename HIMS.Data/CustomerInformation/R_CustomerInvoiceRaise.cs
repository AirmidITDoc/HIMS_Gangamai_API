
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Model.CustomerInformation;
using HIMS.Data.CustomerInformation;
using HIMS.Common.Utility;

namespace HIMS.Data.CustomerInformation
{
    public class R_CustomerInvoiceRaise : GenericRepository, I_CustomerInvoiceRaise
    {
        public R_CustomerInvoiceRaise(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string CustomerInvoiceRaiseInsert(CustomerInvoiceRaiseParam CustomerInvoiceRaiseParam)
        {
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@InvoiceRaisedId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = CustomerInvoiceRaiseParam.customerInvoiceRaiseInsert.ToDictionary();
            disc3.Remove("InvoiceRaisedId");
            var vInvoiceRaisedId = ExecNonQueryProcWithOutSaveChanges("m_insert_CustomerInvoiceRaise", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return vInvoiceRaisedId;
        }
        public bool CustomerInvoiceRaiseUpdate(CustomerInvoiceRaiseParam CustomerInvoiceRaiseParam)
        {

            var disc3 = CustomerInvoiceRaiseParam.customerInvoiceRaiseUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_CustomerInvoiceRaise", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
