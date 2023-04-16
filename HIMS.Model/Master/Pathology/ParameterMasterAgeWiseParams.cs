using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Pathology
{
    public class ParameterMasterAgeWiseParams
    {
        public InsertParameterMasterAgeWise InsertParameterMasterAgeWise { get; set; }
        public UpdateParameterMasterAgeWise UpdateParameterMasterAgeWise { get; set; }
       //// public 


    }
    public class InsertParameterMasterAgeWise
    {
        public long ParaId { get; set; }
        public long SexId { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public long AddedBy { get; set; }
        public int MinAge { get; set; } 
        public int MaxAge { get; set; }
        public string AgeType { get; set; }
     }

    public class UpdateParameterMasterAgeWise
    {
        public int PathparaRangeId { get; set; }
        public long ParaId { get; set; }
        public long SexId { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public long Updatedby { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string AgeType { get; set; }
    }



}
