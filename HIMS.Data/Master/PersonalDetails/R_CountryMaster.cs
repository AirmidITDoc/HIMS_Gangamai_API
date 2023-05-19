using HIMS.Common.Extensions;
using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Master.PersonalDetails
{
    public class R_CountryMaster :GenericRepository, I_CountryMaster
    {
        public R_CountryMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(CountryMasterParams CountryMasterParams)
        {
            var disc1 = CountryMasterParams.CountryMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_CountryMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(CountryMasterParams CountryMasterParams)
        {
            // throw new NotImplementedException();
            var disc = CountryMasterParams.CountryMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_CountryMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
