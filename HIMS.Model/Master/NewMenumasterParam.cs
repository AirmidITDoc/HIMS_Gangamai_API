using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master
{
    public class NewMenumasterParam
    {
        public MenuInsert MenuInsert { get; set; }
        public MenuUpdate MenuUpdate { get; set; }
    }

    public class MenuInsert
    {

        public long Id { get; set; }
        public long UpId { get; set; }
        public string LinkName { get; set; }
        public string Icon { get; set; }
        public string LinkAction { get; set; }
        public int SortOrder { get; set; }
        public int IsActive { get; set; }
        public int Display { get; set; }

    }

    public class MenuUpdate
    {
        public long Id { get; set; }
        public long UpId { get; set; }
        public string LinkName { get; set; }
        public string Icon { get; set; }
        public string LinkAction { get; set; }
        public int SortOrder { get; set; }
        public int IsActive { get; set; }
        public int Display { get; set; }
    }
}