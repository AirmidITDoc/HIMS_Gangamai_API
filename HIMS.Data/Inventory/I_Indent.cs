using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Inventory;

namespace HIMS.Data.Inventory
{
    public interface I_Indent
    {
        public string Insert(IndentParams indentparams);
        public bool Update(IndentParams indentParams);
    }
}
