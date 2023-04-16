using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_AdmissionReg
    {
        public bool Insert(AdmissionParams AdmissionParams);

        public bool Update(AdmissionParams AdmissionParams);

    }
}
