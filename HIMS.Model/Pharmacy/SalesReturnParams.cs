﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class SalesReturnParams
    {
        public SalesReturnHeader salesReturnHeader { get; set; }
        public List<SalesReturnDetail> SalesReturnDetail { get; set; }
        public List<SalesReturn_CurStk_Upt> SalesReturn_CurStk_Upt { get; set; }
        public List<Update_SalesReturnQty_SalesTbl> Update_SalesReturnQty_SalesTbl { get; set; }
        public Update_SalesRefundAmt_SalesHeader Update_SalesRefundAmt_SalesHeader { get; set; }
        public Cal_GSTAmount_SalesReturn Cal_GSTAmount_SalesReturn { get; set; }
        public Insert_ItemMovementReport_Cursor Insert_ItemMovementReport_Cursor { get; set; }
    }   

    public class SalesReturnHeader
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public long SalesId { get; set; }
        public long OP_IP_ID { get; set; }
        public long OP_IP_Type { get; set; }
        public float TotalAmount { get; set; }
        public float VatAmount { get; set; }
        public float DiscAmount { get; set; }
        public float NetAmount { get; set; }
        public float PaidAmount { get; set; }
        public float BalanceAmount { get; set; }
        public bool IsSellted { get; set; }
        public bool IsPrint { get; set; }
        public bool IsFree { get; set; }
        public long UnitID { get; set; }
        public long AddedBy { get; set; }
        public long StoreID { get; set; }
        public long SalesReturnId { get; set; }

    }
    public class SalesReturnDetail
    {
        public long SalesReturnID { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public float UnitMRP { get; set; }
        public long Qty { get; set; }
        public float TotalAmount { get; set; }
        public float VatPer { get; set; }
        public float VatAmount { get; set; }
        public float DiscPer { get; set; }
        public float DiscAmount { get; set; }
        public float GrossAmount { get; set; }
        public float LandedPrice { get; set; }
        public float TotalLandedAmount { get; set; }
        public float PurRate { get; set; }
        public float PurTot { get; set; }
        public long SalesID { get; set; }
        public long SalesDetId { get; set; }
        public bool IsCashOrCredit { get; set; }
        public float CGSTPer { get; set; }
        public float CGSTAmt { get; set; }
        public float SGSTPer { get; set; }
        public float SGSTAmt { get; set; }
        public float IGSTPer { get; set; }
        public float IGSTAmt { get; set; }
        public long StkId { get; set; }

    }

    public class SalesReturn_CurStk_Upt
    {
        public long ItemId { get; set; }
        public long IssueQty { get; set; }
        public long StoreID { get; set; }
        public long StkID { get; set; }

    }
    public class Update_SalesReturnQty_SalesTbl
    {
        public long SalesDetId { get; set; }
        public float ReturnQty { get; set; }
    }
    public class Update_SalesRefundAmt_SalesHeader
    {
        public long SalesReturnId { get; set; }
    }
  
    public class Cal_GSTAmount_SalesReturn
    {
        public long SalesReturnID { get; set; }
    }

    public class Insert_ItemMovementReport_Cursor
    { 
        public long Id { get; set; }
        public long TypeId { get; set; }
    }
}