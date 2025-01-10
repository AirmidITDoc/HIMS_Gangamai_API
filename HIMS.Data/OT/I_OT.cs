using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.CustomerInformation;
using HIMS.Data.OT;

namespace HIMS.Data.OT 
{
    public interface I_OT
    {
       
        public bool SaveConsentMaster(ConsentMasterParam ConsentMasterParam);
        public bool UpdateConsentMaster(ConsentMasterParam ConsentMasterParam);
        public bool CancelConsentMaster(ConsentMasterParam ConsentMasterParam);
        public bool SaveOTBookingRequest(OTBookingRequestParam OTBookingRequestParam);
        public bool UpdateOTBookingRequest(OTBookingRequestParam OTBookingRequestParam);
        public bool CancelOTBookingRequest(OTBookingRequestParam OTBookingRequestParam);
        public bool SaveOTBooking(OTBookingParam OTBookingParam);
        public bool UpdateOTBooking(OTBookingParam OTBookingParam);
        public bool CancelOTBooking(OTBookingParam OTBookingParam);
        public bool SaveOTTableMaster(MOTTableMasterParam MOTTableMasterParam);
        public bool UpdateOTTableMaster(MOTTableMasterParam MOTTableMasterParam);
        public bool CancelOTTableMaster(MOTTableMasterParam MOTTableMasterParam);
        public bool SaveMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam);
        public bool UpdateMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam);
        public bool CancelMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam);
        public bool SaveMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam);
        public bool UpdateMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam);
        public bool CancelMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam);
        public bool SaveMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam);
        public bool UpdateMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam);
        public bool CancelMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam);
        public bool SaveMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam);
        public bool UpdateMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam);
        public bool CancelMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam);



    }
}
