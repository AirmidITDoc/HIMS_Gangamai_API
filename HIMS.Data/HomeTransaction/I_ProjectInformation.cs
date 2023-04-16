using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
   public interface I_ProjectInformation
    {
        bool Update(ProjectInformationParams ProjectInformationParams);
        bool Save(ProjectInformationParams ProjectInformationParams);
    }
}
