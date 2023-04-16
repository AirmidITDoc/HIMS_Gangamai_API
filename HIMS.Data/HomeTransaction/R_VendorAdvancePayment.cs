using HIMS.Common.Extensions;
using HIMS.Common.Utility;
using HIMS.Model.Master;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Transaction
{
  public  class R_VendorAdvancePayment :GenericRepository,I_VendorAdvancePayment
    {
        //private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public R_VendorAdvancePayment(IUnitofWork unitofWork) : base(unitofWork)
        {

            //_unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
            //transaction and connection is open when you inject unitofwork
        }

        public bool Save(VendorAdvancePaymentParams vendorAdvancePaymentParams)
        {
            //throw new NotImplementedException();
            var disc = vendorAdvancePaymentParams.vendorAdvancePaymentInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_T_VendorAdvancePayment", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        
        public bool Update(VendorAdvancePaymentParams vendorAdvancePaymentParams)
        {
            //throw new NotImplementedException();
            var disc = vendorAdvancePaymentParams.vendorAdvancePaymentUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_T_VendorAdvancePayment", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        //public List<dynamic> getIssueTrackingList(VendorAdvancePaymentParams vendorAdvancePaymentParams)
        //{
        //    command.CommandText = "Rtrv_T_IssueTracking_Swist_by_Name";
        //    command.Parameters.AddWithValue("@FromDate", vendorAdvancePaymentParams.getVendor.FromDate);
        //    command.Parameters.AddWithValue("@ToDate", vendorAdvancePaymentParams.getVendor.ToDate);
        //    command.Parameters.AddWithValue("@HospitalName", vendorAdvancePaymentParams.getVendor.HospitalName);

        //    var dataSet = new DataSet();
        //    (new SqlDataAdapter(command)).Fill(dataSet);
        //    command.Parameters.Clear();

        //    return dataSet.Tables[0].ToDynamic();
        //}
    }
}
