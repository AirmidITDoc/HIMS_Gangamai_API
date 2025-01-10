using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class TNursingMedicationChartParam
    {
        public SaveTNursingMedicationChartParam SaveTNursingMedicationChartParams { get; set; }
        public UpdateTNursingMedicationChartParam UpdateTNursingMedicationChartParams { get; set; }
        public CancelTNursingMedicationChartParam CancelTNursingMedicationChartParams { get; set; }

    }

    public class SaveTNursingMedicationChartParam
    {
        public int MedChartId { get; set; }
        public int AdmID { get; set; }
        public DateTime MDate { get; set; }
        public DateTime MTime { get; set; }
      
        public long DurgId { get; set; }
        public long DoseID { get; set; }
        public string Route { get; set; }
        public string Freq { get; set; }
        public string NurseName { get; set; }
        public string DoseName { get; set; }
        public int CreatedBy { get; set; }
      




    }
    public class UpdateTNursingMedicationChartParam
    {
        public int MedChartId { get; set; }
        public int AdmID { get; set; }
        public DateTime MDate { get; set; }
        public DateTime MTime { get; set; }

        public long DurgId { get; set; }
        public long DoseID { get; set; }
        public string Route { get; set; }
        public string Freq { get; set; }
        public string NurseName { get; set; }
        public string DoseName { get; set; }
        public int ModifiedBy { get; set; }
       

    }
    public class CancelTNursingMedicationChartParam
    {
        public long MedChartId { get; set; }
        public long IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }


       


    }




}


    