using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public interface I_DrugMaster
    {
        public bool Insert(DrugMasterParams drugMasterParams);
        public bool Update(DrugMasterParams drugMasterParams);
    }
}
