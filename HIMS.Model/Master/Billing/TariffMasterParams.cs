using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
  public  class TariffMasterParams
    {
        public TariffMasterInsert TariffMasterInsert { get; set; }
        public TariffMasterUpdate TariffMasterUpdate { get; set; }

    }

    public class TariffMasterInsert
    {
        public String TariffName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class TariffMasterUpdate
    {
        public int TariffID { get; set; }
        public String TariffName { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

