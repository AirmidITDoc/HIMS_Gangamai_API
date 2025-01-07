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
        public bool SaveOTTableMaster(MOTTableMasterParam MOTTableMasterParam);
        public bool UpdateOTTableMaster(MOTTableMasterParam MOTTableMasterParam);
        public bool CancelOTTableMaster(MOTTableMasterParam MOTTableMasterParam);
        public bool SaveMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam);
        public bool UpdateMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam);
        public bool CancelMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam);

        public bool SaveTNursingNotes(NursingNoteParam NursingNoteParam);
        public bool UpdateTNursingNotes(NursingNoteParam NursingNoteParam);
        public bool SaveTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam);
        public bool UpdateTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam);
        public bool SaveTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam);
        public bool UpdateTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam);
        public bool CancelTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam);

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
