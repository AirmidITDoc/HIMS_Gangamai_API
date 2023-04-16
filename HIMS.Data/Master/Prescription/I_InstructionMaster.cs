using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public interface I_InstructionMaster
    {
        public bool Insert(InstructionMasterParams instructionMasterParams);
        public bool Update(InstructionMasterParams instructionMasterParams);
    }
}
