using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
    public class ServiceTarriffParams
    {
        public SaveServiceTarriffParams SaveServiceTarriffParams { get; set; }

      
        //////public List<ServiceDetailInsert> ServiceDetailInsert { get; set; }
       

    }

    public class SaveServiceTarriffParams
    {

        public int Old_TariffId { get; set; }
        public int New_TariffId { get; set; }
    }

}
