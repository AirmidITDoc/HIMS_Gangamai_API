using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace HIMS.Model.Administration
{
    public class ClassMasterPara
    {
        public ClassMasterParamsInsert ClassMasterParamsInsert { get; set; }
        public ClassMasterParamsUpdate ClassMasterParamsUpdate { get; set; }
    }
    public class ClassMasterParamsInsert
    {
        public string ClassName { get; set; }

        public bool IsActive { get; set; }
        public long AddedBy { get; set; }
        public DateTime AddedByDate { get; set; }


    }

    public class ClassMasterParamsUpdate
    {
        public long ClassId { get; set; }
        public string ClassName { get; set; }

        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedByDate { get; set; }

    }
}
