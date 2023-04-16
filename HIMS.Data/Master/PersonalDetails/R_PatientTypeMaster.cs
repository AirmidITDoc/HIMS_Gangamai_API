using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public class R_PatientTypeMaster :GenericRepository,I_PatientTypeMaster
    {
        public R_PatientTypeMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(PatientTypeMasterParams PatientTypeMasterParams)
        {
            var disc1 = PatientTypeMasterParams.PatientTypeMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_PatientTypeMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(PatientTypeMasterParams PatientTypeMasterParams)
        {
            // throw new NotImplementedException();
            var disc = PatientTypeMasterParams.PatientTypeMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PatientTypeMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

       
    }
}
