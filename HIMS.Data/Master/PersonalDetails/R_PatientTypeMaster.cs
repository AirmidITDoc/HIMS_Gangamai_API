﻿using HIMS.Common.Utility;
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
            ExecNonQueryProcWithOutSaveChanges("update_PatientTypeMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(PatientTypeMasterParams PatientTypeMasterParams)
        {
            // throw new NotImplementedException();
            var disc = PatientTypeMasterParams.PatientTypeMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_PatientTypeMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

       
    }
}
