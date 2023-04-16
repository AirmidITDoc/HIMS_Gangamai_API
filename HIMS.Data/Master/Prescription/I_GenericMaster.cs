using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public interface I_GenericMaster
    {
        public bool Insert(GenericMasterParams genericMasterParams);
        public bool Update(GenericMasterParams genericMasterParams);
    }
}
