using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.DepartmenMaster
{
    public class DischargeTypeMasterParams
    {
        public DischargeTypeMasterInsert DischargeTypeMasterInsert { get; set; }
        public DischargeTypeMasterUpdate DischargeTypeMasterUpdate { get; set; }

    }

    public class DischargeTypeMasterInsert
    {
        public String DischargeTypeName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class DischargeTypeMasterUpdate
    {
        public int DischargeTypeId { get; set; }
        public String DischargeTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

