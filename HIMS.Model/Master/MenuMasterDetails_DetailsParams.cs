using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master
{
   public class MenuMasterDetails_DetailsParams
    {
        public MenuMasterDetails_DetailsInsert MenuMasterDetails_DetailsInsert { get; set; }
        public MenuMasterDetails_DetailsUpdate MenuMasterDetails_DetailsUpdate { get; set; }
    }

    public class MenuMasterDetails_DetailsInsert
    {
        public long menu_master_detail_detail_master_id { get; set; }
        public long menu_master_detail_detail_master_sr_no { get; set; }
        public long menu_master_detail_detail_sr_no { get; set; }
        public string menu_master_detail_detail_link_name { get; set; }
        public string menu_master_detail_detail_action { get; set; }
        public long menu_master_detail_detail_block { get; set; }
        public long menu_master_detail_detail_display_sr_no { get; set; }
    }

    public class MenuMasterDetails_DetailsUpdate
    {
        public long menu_master_detail_detail_master_id { get; set; }
        public long menu_master_detail_detail_master_sr_no { get; set; }
        public long menu_master_detail_detail_sr_no { get; set; }
        public string menu_master_detail_detail_link_name { get; set; }
        public string menu_master_detail_detail_action { get; set; }
        public long menu_master_detail_detail_block { get; set; }
        public long menu_master_detail_detail_display_sr_no { get; set; }
        public int Menu_Sub_SubID { get; set; }
    }
}

