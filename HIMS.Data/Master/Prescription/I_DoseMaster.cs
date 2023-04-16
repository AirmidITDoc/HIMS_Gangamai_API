using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public interface I_DoseMaster
    {
        public bool Insert(DoseMasterParams doseMasterParams);
        public bool Update(DoseMasterParams doseMasterParams);
    }
}
