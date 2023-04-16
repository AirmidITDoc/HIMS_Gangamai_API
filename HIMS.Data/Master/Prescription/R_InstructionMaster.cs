using HIMS.Common.Utility;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public class R_InstructionMaster:GenericRepository,I_InstructionMaster
    {
        public R_InstructionMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(InstructionMasterParams instructionMasterParams)
        {
            var disc = instructionMasterParams.UpdateInstructionMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_PrescriptionInstructionMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(InstructionMasterParams instructionMasterParams)
        {
            var disc = instructionMasterParams.InsertInstructionMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PrescriptionInstructionMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
