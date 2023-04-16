using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master
{
   public class MenuMasterParams
    {
        public MenuMasterInsert MenuMasterInsert { get; set; }
        public MenuMasterUpdate MenuMasterUpdate { get; set; }
    }

    public class MenuMasterInsert
    {
        public long menu_master_id { get; set; }
        public string menu_master_link_name { get; set; }
        public string menu_master_icon { get; set; }
        public string menu_master_controller { get; set; }
        public string menu_master_action { get; set; }
        public long menu_master_display_sr_no { get; set; }
        public long menu_master_block { get; set; }

    }

    public class MenuMasterUpdate
    {
        public long menu_master_id { get; set; }
        public string menu_master_link_name { get; set; }
        public string menu_master_icon { get; set; }
        public string menu_master_controller { get; set; }
        public string menu_master_action { get; set; }
        public long menu_master_display_sr_no { get; set; }
        public long menu_master_block { get; set; }
    }
}

