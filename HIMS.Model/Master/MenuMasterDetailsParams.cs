using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master
{
   public class MenuMasterDetailsParams
    {

        public MenuMasterDetailsInsert MenuMasterDetailsInsert { get; set; }
        public MenuMasterDetailsUpdate MenuMasterDetailsUpdate { get; set; }
    }

    public class MenuMasterDetailsInsert
    {
        public long menu_master_detail_master_id { get; set; }
        public long menu_master_detail_sr_no { get; set; }
        public long menu_master_detail_display_sr_no { get; set; }
        public long menu_master_detail_block { get; set; }
        public string menu_master_detail_link_name { get; set; }
        public string menu_master_detail_action { get; set; }
    }
    public class MenuMasterDetailsUpdate
    {
        public long menu_master_detail_master_id { get; set; }
        public long menu_master_detail_sr_no { get; set; }
        public long menu_master_detail_display_sr_no { get; set; }
        public long menu_master_detail_block { get; set; }
        public string menu_master_detail_link_name { get; set; }
        public string menu_master_detail_action { get; set; }
        public int Menu_SubID { get; set; }

    }
}

