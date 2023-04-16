using HIMS.Model.Opd;
using System.Collections.Generic;

namespace HIMS.Data.Opd
{
    public interface I_OpdBrowseList
    {
        List<dynamic> GetBrowseOPDBill(BrowseOPDBillParams browseOPDBillParams);
    }
}