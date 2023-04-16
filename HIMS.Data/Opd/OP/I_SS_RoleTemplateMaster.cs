using HIMS.Model.Opd.OP;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd.OP
{
   public interface I_SS_RoleTemplateMaster
    {
        public bool Save(SS_RoleTemplateMasterParams SS_RoleTemplateMasterParams);
        public bool Update(SS_RoleTemplateMasterParams SS_RoleTemplateMasterParams);
    }
}
