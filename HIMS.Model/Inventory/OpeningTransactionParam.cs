using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class OpeningTransactionParam
    {
        public Insert_OpeningTransaction_header_1 Insert_OpeningTransaction_Header_1 { get; set; }
        public List<OpeningTransactionInsert> OpeningTransactionInsert { get; set; }
        public Insert_Update_OpeningTran_ItemStock_1 Insert_Update_OpeningTran_ItemStock_1 { get; set; }
       
    }
    public class Insert_OpeningTransaction_header_1
    {
        public long StoreId { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime OpeningTime { get; set; }
        public long Addedby { get; set; }
        public long OpeningHId { get; set; }
    }
    public class OpeningTransactionInsert

    //public long StoreId { get; set; }
    //public DateTime OpeningDate { get; set; }
    //public DateTime OpeningTime { get; set; }
    //public long OpeningDocNo { get; set; }
    //public long ItemId { get; set; }
    //public string BatchNo { get; set; }
    //public DateTime BatchExpDate { get; set; }
    //public long PerUnitPurRate { get; set; }
    //public long PerUnitMrp { get; set; }
    //public long VatPer { get; set; }
    //public long BalQty { get; set; }
    //public long Addedby { get; set; }
    //public long OpeningId { get; set; }
   {

       public long StoreId { get; set; }   
    public DateTime OpeningDate { get; set; }           
    public DateTime OpeningTime { get; set; }    
    public long OpeningDocNo { get; set; }               
    public long ItemId { get; set; }    
    public string BatchNo { get; set; }  
    public DateTime BatchExpDate { get; set; }    
    public long PerUnitPurRate { get; set; }      
    public long PerUnitMrp { get; set; }      
    public long VatPer { get; set; }     
    public long BalQty { get; set; }     
    public long Addedby { get; set; } 
    public long updatedby { get; set; }
    public long OpeningId { get; set; }              
              
}
    
    public class Insert_Update_OpeningTran_ItemStock_1
    {
        public long OPeningHId { get; set; }
        
    }
}




