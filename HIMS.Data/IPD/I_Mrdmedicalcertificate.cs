using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_Mrdmedicalcertificate
    {
        public bool Insert(Mrdmedicalcertificateparam Mrdmedicalcertificateparam);
       public bool Update(Mrdmedicalcertificateparam Mrdmedicalcertificateparam);
    }
}
