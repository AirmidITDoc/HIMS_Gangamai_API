using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.CustomerInformation;
using HIMS.Data.Nursing;
using HIMS.Common.Utility;

namespace HIMS.Data.Nursing
{
    public class R_Nursing : GenericRepository, I_Nursing
    {
        public R_Nursing(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool SaveMTemplateMaster(MTemplateMasterParam MTemplateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MTemplateMasterParam.SaveMTemplateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_TemplateMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMTemplateMaster(MTemplateMasterParam MTemplateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MTemplateMasterParam.UpdateMTemplateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_TemplateMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelMTemplateMaster(MTemplateMasterParam MTemplateMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MTemplateMasterParam.CancelMTemplateMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_cancel_M_TemplateMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveTNursingNotes(NursingNoteParam NursingNoteParam)
        {
            // throw new NotImplementedException();
            var disc = NursingNoteParam.SaveNursingNoteParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_T_Nursing_Note", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateTNursingNotes(NursingNoteParam NursingNoteParam)
        {
            // throw new NotImplementedException();
            var disc = NursingNoteParam.UpdateNursingNoteParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_Nursing_Note", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingPatientHandoverParam.SaveTNursingPatientHandoverParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_Nursing_PatientHandover", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingPatientHandoverParam.UpdateTNursingPatientHandoverParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_Nursing_PatientHandover", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool SaveTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingMedicationChartParam.SaveTNursingMedicationChartParams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_Nursing_MedicationChart", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingMedicationChartParam.UpdateTNursingMedicationChartParams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_Nursing_MedicationChart", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool CancelTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
            // throw new NotImplementedException();
            var disc = TNursingMedicationChartParam.CancelTNursingMedicationChartParams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cancel_T_Nursing_MedicationChart", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
