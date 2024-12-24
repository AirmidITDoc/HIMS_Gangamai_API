using HIMS.Model.CustomerInformation;
using HIMS.Model.HomeDelivery;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.CustomerInformation
{
    public  interface I_CustomerInformation
    {
        public string CustomerInformationInsert(CustomerInformationParams customerInformationParams);
        public bool CustomerInformationUpdate(CustomerInformationParams customerInformationParams);
        public bool SaveVendorInformation(VendorInformationParam VendorInformationParam);
        public bool UpdateVendorInformation(VendorInformationParam VendorInformationParam);



        public bool SaveCertificateMaster(CertificateMasterParam CertificateMasterParam);
        public bool UpdateCertificateMaster(CertificateMasterParam CertificateMasterParam);
        public bool CancelCertificateMaster(CertificateMasterParam CertificateMasterParam);
        public bool SaveConsentMaster(ConsentMasterParam ConsentMasterParam);
        public bool UpdateConsentMaster(ConsentMasterParam ConsentMasterParam);
        public bool CancelConsentMaster(ConsentMasterParam ConsentMasterParam);
        public bool SaveOTBookingRequest(OTBookingRequestParam OTBookingRequestParam);
        public bool UpdateOTBookingRequest(OTBookingRequestParam OTBookingRequestParam);
        public bool CancelOTBookingRequest(OTBookingRequestParam OTBookingRequestParam);
        public bool SaveOTBooking(OTBookingParam OTBookingParam);
        public bool UpdateOTBooking(OTBookingParam OTBookingParam);
        public bool CancelOTBooking(OTBookingParam OTBookingParam);
    }
}
