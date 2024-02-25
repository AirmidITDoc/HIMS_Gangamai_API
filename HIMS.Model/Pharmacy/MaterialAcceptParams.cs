using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class MaterialAcceptParams
    {
        public MaterialAccept MaterialAccept { get; set; }
    }

    public class MaterialAccept
    {
        public int IssueId { get; set; }
        public int AcceptedBy { get; set; }
        
    }

}
