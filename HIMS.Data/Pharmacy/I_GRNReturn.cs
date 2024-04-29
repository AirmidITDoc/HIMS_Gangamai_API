using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public interface I_GRNReturn
    {
        public string InsertGRNReturn(GRNReturnParam GRNReturnParam);
        public bool VerifyGRNReturn(GRNReturnParam GRNReturnParam);
    }
}
