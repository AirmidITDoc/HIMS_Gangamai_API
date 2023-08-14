using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Prescription
{
    public class InstructionMasterParams
    {
        public InsertInstructionMaster InsertInstructionMaster { get; set; }
        public UpdateInstructionMaster UpdateInstructionMaster { get; set; }
    }
    public class InsertInstructionMaster
    {
        public string InstructionName { get; set; }
        public bool IsActive { get; set; }
        //public string InstructionHindi { get; set; }
    }
    public class UpdateInstructionMaster
    {
        public long InstructionId { get;set;}
        public string InstructionName { get; set; }
        public bool IsActive { get; set; }
       // public string InstructionHindi { get; set; }
	
    }
}
