using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
   public class ProjectInformationParams
    {
        public ProjectInformationInsert ProjectInformationInsert { get; set; }
        public ProjectInformationUpdate ProjectInformationUpdate { get; set; }
    }

    public class ProjectInformationInsert
    {
        public string ProjectName { get; set; }
        public string ProjectOwner { get; set; }
        public string ProjectDesc { get; set; }
        public string ProSiteName { get; set; }
        public Boolean IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }

    public class ProjectInformationUpdate
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectOwner { get; set; }
        public string ProjectDesc { get; set; }
        public string ProSiteName { get; set; }
        public Boolean IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }

}

