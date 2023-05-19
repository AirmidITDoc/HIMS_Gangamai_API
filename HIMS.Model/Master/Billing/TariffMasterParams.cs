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
        public bool IsActive { get; set; }

    }

    public class TariffMasterUpdate
    {
        public int TariffId { get; set; }
        public String TariffName { get; set; }
        public Boolean IsActive { get; set; }
        
    }
}

