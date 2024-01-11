using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Users
{
    public class RoleModel
    {
        public Int64 RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
