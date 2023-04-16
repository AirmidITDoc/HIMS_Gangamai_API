using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd.OP
{
    public class SS_RoleTemplateMasterParams
    {
        public SS_RoleTemplateMasterSave SS_RoleTemplateMasterSave { get; set; }
        
        public List<SS_RoleTemplateDetailSave> SS_RoleTemplateDetailSave { get; set; }

        public SS_RoleTemplateMasterUpdate SS_RoleTemplateMasterUpdate { get; set; }

        public SS_RoleTemplateDetailDelete SS_RoleTemplateDetailDelete { get; set; }
    }


    public class SS_RoleTemplateMasterSave
    {
        public int RoleId { get; set; }
        public String RoleName { get; set; }
        public Boolean IsDeleted { get; set; }

        public int AddedBy { get; set; }
        public DateTime AddedByDate { get; set; }

    }

    public class SS_RoleTemplateMasterUpdate
    {
        public int RoleId { get; set; }
        public String RoleName { get; set; }
        public Boolean IsDeleted { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedByDate { get; set; }
    }
   
    public class SS_RoleTemplateDetailSave
    {
        public int RoleId { get; set; }

        public int menu_master_id { get; set; }

        public String menu_master_icon { get; set; }

        public String menu_master_link_name { get; set; }

        public String menu_master_action { get; set; }

        public String menu_master_detail_link_name { get; set; }
        
        public String menu_master_detail_action { get; set; }

        public String menu_master_detail_detail_link_name { get; set; }

        public String menu_master_detail_detail_action { get; set; }

        public String menu_master_detail_detail_icon { get; set; }

        public String menu_master_detail_icon { get; set; }
        public int AddedBy { get; set; }

        public int RoleDetId { get; set; }

        public DateTime AddedByDate { get; set; }

    }

    public class SS_RoleTemplateDetailDelete
    {
        public int RoleId { get; set; }
    }

}