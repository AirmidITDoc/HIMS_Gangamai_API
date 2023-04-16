using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public interface I_RelationshipMaster
    {
        bool Update(RelationshipMasterParams RelationshipMasterParams);
        bool Save(RelationshipMasterParams RelationshipMasterParams);
    }
}
