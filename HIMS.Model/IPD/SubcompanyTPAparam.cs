using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class SubcompanyTPAparam
    {
        public InsertsubcompanyTPA InsertsubcompanyTPA { get; set; }
        public UpdatesubcompanyTPA UpdatesubcompanyTPA { get; set; }

    }

   
    public class InsertsubcompanyTPA
    {

         public int CompTypeId { get; set; }

        public String CompanyName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String PinNo { get; set; }
        public String PhoneNo { get; set; }
        public String MobileNo { get; set; }
        public String FaxNo { get; set; }

    public bool IsActive { get; set; }
    public int AddedBy { get; set; }
    public int UpdatedBy { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsCancelledBy { get; set; }
    public DateTime IsCancelledDate { get; set; }


}
    public class UpdatesubcompanyTPA
    {


        public int SubCompanyId { get; set; }
        public int CompTypeId { get; set; }

        public String CompanyName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String PinNo { get; set; }
        public String PhoneNo { get; set; }
        public String MobileNo { get; set; }
        public String FaxNo { get; set; }

        public bool IsActive { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }


    }
}