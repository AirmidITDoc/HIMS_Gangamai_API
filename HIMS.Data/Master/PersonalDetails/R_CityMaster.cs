using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public class R_CityMaster :GenericRepository ,I_CityMaster
    {
        public R_CityMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
        
        public bool Update(CityMasterParams CityMasterParams)
        {
            var disc1 = CityMasterParams.CityMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_M_CityMaster_1", disc1);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(CityMasterParams CityMasterParams)
        {
            // throw new NotImplementedException();
            var disc = CityMasterParams.CityMasterInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_M_CityMaster_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        /*public bool Update(CityMasterParams CityMasterParams)
        {
            throw new NotImplementedException();
        }

        public bool Save(CityMasterParams CityMasterParams)
        {
            throw new NotImplementedException();
        }*/
    }
}
