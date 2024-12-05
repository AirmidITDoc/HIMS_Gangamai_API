using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class NursingOrygenVentilatorParam
    {
        public SaveNursingOrygenVentilatorParam SaveNursingOrygenVentilatorParam { get; set; }
        public UpdateNursingOrygenVentilatorParam UpdateNursingOrygenVentilatorParam { get; set; }
    }
    public class SaveNursingOrygenVentilatorParam
    {
       
        public DateTime EntryDate { get; set; }
        public DateTime EntryTime { get; set; }
        public long AdmissionId { get; set; }
        public long Mode { get; set; }
        public string TidolV { get; set; }
        public string SetRange { get; set; }
        public string IPAP { get; set; }
        public string MinuteV { get; set; }
        public string RateTotal { get; set; }
        public string EPAP { get; set; }
        public string Peep { get; set; }
        public string PC { get; set; }
        public string MVPercentage { get; set; }
        public string PrSup { get; set; }
        public string FIO2 { get; set; }
        public string IE { get; set; }
        public string OxygenRate { get; set; }
        public string SaturationWithO2 { get; set; }
        public string FlowTrigger { get; set; }
        public long CreatedBy { get; set; }
       

    }

    public class UpdateNursingOrygenVentilatorParam
    {
        public long Id { get; set; }
        public long AdmissionId { get; set; }
        public long Mode { get; set; }
        public string TidolV { get; set; }
        public string SetRange { get; set; }
        public string IPAP { get; set; }
        public string MinuteV { get; set; }
        public string RateTotal { get; set; }
        public string EPAP { get; set; }
        public string Peep { get; set; }
        public string PC { get; set; }
        public string MVPercentage { get; set; }
        public string PrSup { get; set; }
        public string FIO2 { get; set; }
        public string IE { get; set; }
        public string OxygenRate { get; set; }
        public string SaturationWithO2 { get; set; }
        public string FlowTrigger { get; set; }
        public long ModifiedBy { get; set; }
    }
}
