﻿namespace HIMS.Model.Master
{
    public class MenuMaster
    {
        public int Id { get; set; }
        public int UpId { get; set; }
        public string LinkName { get; set; }
        public string Icon { get; set; }
        public string LinkAction { get; set; }
        public double SortOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisplay { get; set; }
    }
}