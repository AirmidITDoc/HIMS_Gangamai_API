using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class ConstantsParam
    {
        public InsertConstantsParam InsertConstantsParam { get; set; }
        public UpdateConstantsParam updateConstantsParam { get; set; }


    }
    public class InsertConstantsParam
    {
        public long ConstantId { get; set; }

        public string Name { get; set; }
        public string value { get; set; }
        public string ConstantType { get; set; }
        public long IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }
    public class UpdateConstantsParam
    {
        public long ConstantId { get; set; }
        public string Name { get; set; }
       public string value { get; set; }
       public string ConstantType { get; set; }
       public long IsActive { get; set; }
       public long CreatedBy { get; set; }
       public DateTime CreatedOn { get; set; }
        


    }

}
