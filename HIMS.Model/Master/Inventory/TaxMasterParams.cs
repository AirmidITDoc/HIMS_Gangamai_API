using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class TaxMasterParams
    {
        public InsertTaxMaster InsertTaxMaster { get; set; }
        public UpdateTaxMaster UpdateTaxMaster { get; set; }
    }
    public class InsertTaxMaster
    {
        public string TaxNature { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }             
    }
    public class UpdateTaxMaster
    {
        public long Id { get; set; }
        public string TaxNature { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
