using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class MExpensesHeadMasterParam
    {
        public SaveMExpensesHeadMasterParam SaveMExpensesHeadMasterParam { get; set; }
        public UpdateMExpensesHeadMasterParam UpdateMExpensesHeadMasterParam { get; set; }

    }
    public class SaveMExpensesHeadMasterParam
    {
       
        public string HeadName { get; set; }
       
        public long IsDeleted { get; set; }
        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long ExpHedId { get; set; }

    }
    public class UpdateMExpensesHeadMasterParam
    {
        public long ExpHedId { get; set; }
        public string HeadName { get; set; }

        public long IsDeleted { get; set; }
        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }
      

    }


}
