using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
   public class Stockadjustmentparam
    {
        public InsertMRPStockadju InsertMRPStockadju { get; set; }
        public Updatecurrentstockadjyadd Updatecurrentstockadjyadd { get; set; }

        public Updatecurrentstockadjydedu Updatecurrentstockadjydedu { get; set; }

        public Insertitemmovstockadd Insertitemmovstockadd { get; set; }
        public Insertitemmovstockdedu Insertitemmovstockdedu { get; set; }


    }

    public class InsertMRPStockadju
    {


        public int StoreID { get; set; }

        public int ItemId { get; set; }

        public String BatchNo { get; set; }
        public int Ad_DD_Type { get; set; }


        public float Ad_DD_Qty { get; set; }
        public float PreBalQty { get; set; }

        public float AfterBalQty { get; set; }
        public float MRPAdg { get; set; }


        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }

        public DateTime Date { get; set; }
        public DateTime Time { get; set; }


        public int StockAdgId { get; set; }



    }

    public class Updatecurrentstockadjyadd
    {

        public int StoreId { get; set; }

        public int StockId { get; set; }

        public int ItemId { get; set; }
        public float ReceivedQty { get; set; }


    }

    public class Updatecurrentstockadjydedu
    {

        public int StoreId { get; set; }

        public int StockId { get; set; }

        public int ItemId { get; set; }
        public float ReceivedQty { get; set; }


    }


    public class Insertitemmovstockadd
    {

        public int Id { get; set; }

        public int TypeId { get; set; }



    }

    public class Insertitemmovstockdedu
    {

        public int Id { get; set; }

        public int TypeId { get; set; }


    }
}