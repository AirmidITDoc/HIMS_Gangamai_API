using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class TExpenseParam
    {
        public  insert_T_Expense insert_T_Expense { get; set; }
        public Update_T_Expenses_IsCancel Update_T_Expenses_IsCancel { get; set; }
    }
    public class insert_T_Expense
    {
        //public long ExpId { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime ExpTime { get; set; }
        public long ExpType { get; set; }
        public float ExpAmount { get; set; }
        public string PersonName { get; set; }
        public string Narration { get; set; }
        public long IsAddedby { get; set; }
        public long IsCancelled { get; set; }
        public long VoucharNo { get; set; }
        public long ExpHeadId { get; set; }

    }

    public class Update_T_Expenses_IsCancel
    {
        public long ExpId { get; set; }
        public long IsCancelledBy { get; set; }
      
    }
}
