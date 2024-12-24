using System.Data.SqlClient;
using System.Data;
using HIMS.Common.Utility;
using HIMS.Model.CustomerInformation;
using HIMS.Data.CustomerInformation;

namespace HIMS.Data.CustomerInformation
{
    public class R_CustomerInformation : GenericRepository, I_CustomerInformation
    {
        public R_CustomerInformation(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool SaveOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingRequestParam.SaveOTBookingRequestParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_OTBooking_Request", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingRequestParam.UpdateOTBookingRequestParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_OTBooking_Request", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingRequestParam.CancelOTBookingRequestParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cancel_T_OTBooking_Request", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveOTBooking(OTBookingParam OTBookingParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingParam.SaveOTBookingParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_OTBooking", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateOTBooking(OTBookingParam OTBookingParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingParam.UpdateOTBookingParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_OTBooking", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelOTBooking(OTBookingParam OTBookingParam)
        {
            // throw new NotImplementedException();
            var disc = OTBookingParam.CancelOTBookingParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cancel_T_OTBooking", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveCertificateMaster(CertificateMasterParam CertificateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = CertificateMasterParam.SaveCertificateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_CertificateMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateCertificateMaster(CertificateMasterParam CertificateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = CertificateMasterParam.UpdateCertificateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_CertificateMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelCertificateMaster(CertificateMasterParam CertificateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = CertificateMasterParam.CancelCertificateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cancel_M_CertificateMaster ", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }


        public bool SaveConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            // throw new NotImplementedException();
            var disc = ConsentMasterParam.SaveConsentMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_ConsentMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            // throw new NotImplementedException();
            var disc = ConsentMasterParam.UpdateConsentMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_ConsentMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            // throw new NotImplementedException();
            var disc = ConsentMasterParam.CancelConsentMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cancel_M_ConsentMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveVendorInformation(VendorInformationParam VendorInformationParam)
        {
            // throw new NotImplementedException();
            var disc = VendorInformationParam.SaveVendorInformationParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_VendorInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateVendorInformation(VendorInformationParam VendorInformationParam)
        {
            // throw new NotImplementedException();
            var disc = VendorInformationParam.UpdateVendorInformationParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_VendorInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public string CustomerInformationInsert(CustomerInformationParams customerInformationParams)
        {
            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@CustomerId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = customerInformationParams.CustomerInformationInsert.ToDictionary();
            disc3.Remove("CustomerId");
            var vCustomerId = ExecNonQueryProcWithOutSaveChanges("m_insert_CustomerInformation", disc3, vIndentId);

            _unitofWork.SaveChanges();
            return vCustomerId;
        }
        public bool CustomerInformationUpdate(CustomerInformationParams customerInformationParams)
        {
           
            var disc3 = customerInformationParams.CustomerInformationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_CustomerInformation", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
