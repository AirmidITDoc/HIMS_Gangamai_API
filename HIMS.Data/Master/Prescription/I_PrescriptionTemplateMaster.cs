using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
   public interface I_PrescriptionTemplateMaster
    {
        public bool Insert(PrescriptionTemplateMasterParams PrescriptionTemplateMasterParams);
        public bool Update(PrescriptionTemplateMasterParams PrescriptionTemplateMasterParams);

    }
}
