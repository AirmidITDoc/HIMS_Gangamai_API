using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class ManufactureMasterParams
    {
        public InsertManufactureMaster InsertManufactureMaster { get; set; }
        public UpdateManufactureMaster UpdateManufactureMaster { get; set; }
    }

    public class InsertManufactureMaster
    {
        public string ManufName { get; set; }
        public string ManufShortName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }

    }

    public class UpdateManufactureMaster
    {
        public long ManufId { get; set; }
        public string ManufName { get; set; }
        public string ManufShortName { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }

}
