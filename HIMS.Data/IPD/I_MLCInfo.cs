using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_MLCInfo
    {
        public bool Insert(MLCInfoParams MLCInfoParams);

        public bool Update(MLCInfoParams MLCInfoParams);
    }
}
