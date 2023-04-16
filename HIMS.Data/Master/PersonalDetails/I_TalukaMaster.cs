using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public interface I_TalukaMaster
    {
        
        bool Update(TalukaMasterParams TalukaMasterParams);
        bool Save(TalukaMasterParams TalukaMasterParams);
    }
}
