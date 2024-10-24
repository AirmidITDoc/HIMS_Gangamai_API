using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
   public class HelthCardParam
   {

     public InsertHelthCardParam InsertHelthCardParam { get; set; }

   }

    public class InsertHelthCardParam
    {
        public long HealthCardNo { get; set; }
        public DateTime CardDate { get; set; }
    }

}
