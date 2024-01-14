using System.Collections.Generic;

namespace HIMS.Model.Master
{
    public class MenuMaster
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int UpId { get; set; }
        public string LinkName { get; set; }
        public string Icon { get; set; }
        public string LinkAction { get; set; }
        public double SortOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisplay { get; set; }
        public string Parent { get; set; }
        public int RoleId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }

    public class MenuModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string translate { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
        public List<MenuModel> children { get; set; }
    }
}