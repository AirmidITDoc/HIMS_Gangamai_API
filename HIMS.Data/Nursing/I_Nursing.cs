using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.CustomerInformation;
using HIMS.Data.Nursing;

namespace HIMS.Data.Nursing
{
    public interface I_Nursing
    {
        public bool SaveTNursingNotes(NursingNoteParam NursingNoteParam);
        public bool UpdateTNursingNotes(NursingNoteParam NursingNoteParam);
        public bool SaveTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam);
        public bool UpdateTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam);
        public bool SaveTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam);
        public bool UpdateTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam);
        public bool CancelTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam);
    }
}
