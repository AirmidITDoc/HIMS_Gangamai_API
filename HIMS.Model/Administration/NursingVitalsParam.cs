using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class NursingVitalsParam
    {
        public SaveNursingVitalsParam SaveNursingVitalsParam { get; set; }
        public UpdateSaveNursingVitalsParam UpdateSaveNursingVitalsParam { get; set; }
    }
    public class SaveNursingVitalsParam
    {
       
        public DateTime VitalDate { get; set; }
        public DateTime VitalTime { get; set; }
        public long AdmissionId { get; set; }
        public string Temperature { get; set; }
        public string Pulse { get; set; }
        public string Respiration { get; set; }
        public string BloodPresure { get; set; }
        public string CVP { get; set; }
        public string Peep { get; set; }
        public string ArterialBloodPressure { get; set; }
        public string PAPressureReading { get; set; }
        public string Brady { get; set; }
        public string Apnea { get; set; }
        public string AbdominalGrith { get; set; }
        public string Desaturation { get; set; }
        public string SaturationWithO2 { get; set; }
        public string SaturationWithoutO2 { get; set; }
        public string PO2 { get; set; }
        public string FIO2 { get; set; }
        public string PFRation { get; set; }
        public long SuctionType { get; set; }
        public long CreatedBy { get; set; }

    }

    public class UpdateSaveNursingVitalsParam
    {
        public long VitalId { get; set; }
        public long AdmissionId { get; set; }
        public string Temperature { get; set; }
        public string Pulse { get; set; }
        public string Respiration { get; set; }
        public string BloodPresure { get; set; }
        public string CVP { get; set; }
        public string Peep { get; set; }
        public string ArterialBloodPressure { get; set; }
        public string PAPressureReading { get; set; }
        public string Brady { get; set; }
        public string Apnea { get; set; }
        public string AbdominalGrith { get; set; }
        public string Desaturation { get; set; }
        public string SaturationWithO2 { get; set; }
        public string SaturationWithoutO2 { get; set; }
        public string PO2 { get; set; }
        public string FIO2 { get; set; }
        public string PFRation { get; set; }
        public long SuctionType { get; set; }
        public long ModifiedBy { get; set; }
    }
}
