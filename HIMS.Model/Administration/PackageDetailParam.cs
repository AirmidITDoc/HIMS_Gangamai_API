using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class PackageDetailParam
    {
        public Delete_PackageDetails Delete_PackageDetails { get; set; }
        public List<insert_PackageDetails> insert_PackageDetails { get; set; }
    }
    public class Delete_PackageDetails
    {
        public long ServiceId { get; set; }
      
    }

    public class insert_PackageDetails
    {
        public long ServiceId { get; set; }
        public long PackageServiceId { get; set; }
    }
}
