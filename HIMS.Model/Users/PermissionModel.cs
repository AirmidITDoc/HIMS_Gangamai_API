using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Users
{
    public class PermissionModel
    {
        public Int64 RoleId { get; set; }
        public int MenuId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }
}
