using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public class R_RelationshipMaster :GenericRepository,I_RelationshipMaster
    {
        public R_RelationshipMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(RelationshipMasterParams RelationshipMasterParams)
        {
            //throw new NotImplementedException();
            var disc1 = RelationshipMasterParams.RelationshipMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_RelationshipMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(RelationshipMasterParams RelationshipMasterParams)
        {
            //throw new NotImplementedException();
            var disc = RelationshipMasterParams.RelationshipMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_RelationshipMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
