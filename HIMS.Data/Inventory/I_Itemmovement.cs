using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Inventory
{
  public  interface I_Itemmovement
    {
        
             string ViewItemmovement(DateTime FromDate, DateTime ToDate, int ItemId, int FromStoreID, int ToStoreId, string htmlFilePath, string htmlHeaderFilePath);

    }
}
