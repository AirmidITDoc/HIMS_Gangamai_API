using HIMS.Common.Utility;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
    public class R_PrescriptionClassMaster:GenericRepository,I_PrescriptionClassMaster
    {
        public R_PrescriptionClassMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(PrescriptionClassMasterParams presClassMasterParams)
        {
            var disc = presClassMasterParams.UpdatePrescriptionClassMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("  ", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(PrescriptionClassMasterParams presClassMasterParams)
        {
            var disc = presClassMasterParams.InsertPrescriptionClassMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("  ", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
