using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Pathology
{
   public class PathParameterMasterParams 
    {
        public PathParameterMasterInsert PathParameterMasterInsert { get; set; }
        public List<ParameterRangeWithAgeMasterInsert> ParameterRangeWithAgeMasterInsert { get; set; }
        public List<ParameterDescriptiveMasterInsert> ParameterDescriptiveMasterInsert { get; set; }
        public PathParameterMasterUpdate PathParameterMasterUpdate { get; set; }
        public DescriptiveParameterMasterDelete DescriptiveParameterMasterDelete { get; set; }
        public ParameterRangeWithAgeMasterDelete ParameterRangeWithAgeMasterDelete { get; set; }
    }

    public class PathParameterMasterInsert
    {
         public string ParameterShortName { get; set; }
        public string ParameterName { get; set; }
        public string PrintParameterName { get; set; }
        public int UnitId { get; set; }
        public bool IsNumeric { get; set; }
        public bool IsDeleted { get; set; }
        public int Addedby { get; set; }
        public bool IsPrintDisSummary { get; set; }
        public string MethodName { get; set; }
        public int ParameterID { get; set; }

    }
    public class ParameterRangeWithAgeMasterInsert
    {
        public int ParaId { get; set; }
        public int SexId { get; set; }
        public string MinValue { get; set; }
        public string Maxvalue { get; set; }
        public int Addedby { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string AgeType { get; set; }

    }
    public class ParameterDescriptiveMasterInsert
    {
        public int ParameterID { get; set; }
        public string ParameterValues { get; set; }
        public bool IsDefaultValue { get; set; }
        public int Addedby { get; set; }
        public string DefaultValue { get; set; }

    }
    public class DescriptiveParameterMasterDelete
    {
        public int ParameterId { get; set; }
    }


    public class ParameterRangeWithAgeMasterDelete
    {
        public int ParameterId { get; set; }
    }
    public class PathParameterMasterUpdate
    {
        public string ParameterShortName { get; set; }
        public string ParameterName { get; set; }
        public string PrintParameterName { get; set; }
        public int UnitId { get; set; }
        public bool IsNumeric { get; set; }
        public bool IsDeleted { get; set; }
        public int Updatedby { get; set; }
        public bool IsPrintDisSummary { get; set; }
        public string MethodName { get; set; }
        public int ParameterID { get; set; }

    }
}

