using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Inventory
{
   public interface I_ReturnFromDept
    {
        public string InsertReturnFromDepartment(ReturnfrdeptParam ReturnfrdeptParam);

    }
}
