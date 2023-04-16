using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public interface I_PrescriptionClassMaster
    {
        public bool Insert(PrescriptionClassMasterParams presClassMasterParams);
        public bool Update(PrescriptionClassMasterParams presClassMasterParams);
    }
}
