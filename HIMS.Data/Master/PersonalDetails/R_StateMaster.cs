using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
    public class R_StateMaster : GenericRepository, I_StateMaster
    {
        public R_StateMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(StateMasterParams StateMasterParams)
        {
            var disc1 = StateMasterParams.StateMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_StateMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(StateMasterParams StateMasterParams)
        {
            // throw new NotImplementedException();
            var disc = StateMasterParams.StateMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_StateMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
    }
