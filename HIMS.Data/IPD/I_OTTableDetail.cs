using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_OTTableDetail
    {
        public bool Insert(OTTableDetailParams OTTableDetailParams);

        public bool Update(OTTableDetailParams OTTableDetailParams);
    }
}
