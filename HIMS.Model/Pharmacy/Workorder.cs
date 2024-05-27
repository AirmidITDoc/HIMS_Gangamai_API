using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
   public class Workorder
    {
        public WorkorderHeaderInsert WorkorderHeaderInsert { get; set; }
        public List<WorkorderDetailInsert> WorkorderDetailInsert { get; set; }
        public UpdateWorkOrderHeader UpdateWorkOrderHeader { get; set; }
        public Delete_WorkDetails Delete_WorkDetails { get; set; }
    }
    public class WorkorderHeaderInsert
    {

        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public long StoreId { get; set; }
        public long SupplierID { get; set; }
        public float TotalAmount { get; set; }
        public float VatAmount { get; set; }
        public float DiscAmount { get; set; }
        public float NetAmount { get; set; }
        public bool Isclosed { get; set; }
        public string Remarks { get; set; }
        public long AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
       
        public float WOId { get; set; }
      }

    public class WorkorderDetailInsert
    {
        public long WOId { get; set; }
        public string ItemName { get; set; }
        public long Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscPer { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public decimal NetAmount { get; set; }
        public long Remark { get; set; }

    }

    public class UpdateWorkOrderHeader
    {
        public long WOId { get; set; }
        public long StoreId { get; set; }
        public long SupplierID { get; set; }
        public float TotalAmount { get; set; }
        public float DiscAmount { get; set; }
        public float VatAmount { get; set; }
        public float NetAmount { get; set; }
        public bool Isclosed { get; set; }
        public string Remarks { get; set; }
        public long UpdatedBy { get; set; }
     
    }
    public class Delete_WorkDetails
    {
        public int WOID { get; set; }
        
    }

   
}
