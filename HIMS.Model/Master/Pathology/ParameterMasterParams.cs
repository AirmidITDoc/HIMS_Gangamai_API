using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Pathology
{ 
    public class ParameterMasterParams
    {
        public InsertParameterMaster InsertParameterMaster { get; set; }
        public UpdateParameterMaster UpdateParameterMaster { get; set; }

        public UpdateParameterMasterRangeWise UpdateParameterMasterRangeWise { get; set; }
        public DeleteAssignParameterToRange DeleteAssignParameterToRange { get; set; }
        public InsertParameterMasterRangeWise InsertParameterMasterRangeWise { get; set; }
        public DeleteAssignParameterToDescriptive DeleteAssignParameterToDescriptive { get; set; }
        public List<InsertAssignParameterToDescriptive> InsertAssignParameterToDescriptives { get; set; }
        
       
    }

       public class InsertParameterMaster
    {
        public string ParameterShortName { get; set; }
        public string ParameterName { get; set; }
        public string PrintParameterName { get; set; }
        public long UnitId { get; set; }
        public int IsNumeric { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
        public bool IsPrintDisSummary { get; set; }
        public string MethodName { get; set; }
        public string ParaMultipleRange { get; set; }
        public int ParameterID { get; set; }

    }

    public class InsertParameterMasterRangeWise
    {
        public int ParaId { get; set; }
        public int SexId { get; set; }
        public string MinValue { get; set; }
        public string Maxvalue { get; set; }

        public bool IsDeleted { get; set; }
        public int Addedby { get; set; }

    }
 

    public class UpdateParameterMasterRangeWise
    {
        public int PathparaRangeId { get; set; }
        public int ParaId { get; set; }
        public int SexId { get; set; }
      
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }

    }


    public class DeleteAssignParameterToRange
    {
        public int ParaId { get; set; }

    }

        public class DeleteAssignParameterToDescriptive
    {
        public int ParameterId { get; set; }
    }

    public class InsertAssignParameterToDescriptive
    {
        public int ParameterId { get; set; }
        public string ParameterValues { get; set; }
        public Boolean IsDefaultValue { get; set; }
        public int AddedBy { get; set; }

        public string DefaultValue { get; set; }
    }
    public class UpdateParameterMaster
    {
        public int ParameterID { get; set; }
        public string ParameterShortName { get; set; }
        public string ParameterName { get; set; }
        public string PrintParameterName { get; set; }
        public long UnitId { get; set; }
        public int IsNumeric { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsPrintDisSummary { get; set; }

        public string ParaMultipleRange { get; set; }
        public string MethodName { get; set; }
    }


    


}
