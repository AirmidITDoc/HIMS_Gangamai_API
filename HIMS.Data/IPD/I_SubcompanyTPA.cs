using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
public interface I_SubcompanyTPA
    {
        public bool Insert(SubcompanyTPAparam SubcompanyTPAparam);

        public bool Update(SubcompanyTPAparam SubcompanyTPAparam);
    }
}
