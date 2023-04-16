using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
    public interface I_BankMaster1
    {
        List<dynamic> Get(string BankName);

        bool Update(BankMaster1 bankmaster);
        bool Insert(BankMaster1 bankMaster);
    }
}
