using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class TalukaMasterParams
    {
        public TalukaMasterInsert TalukaMasterInsert { get; set; }
        public TalukaMasterUpdate TalukaMasterUpdate { get; set; }

    }

    public class TalukaMasterInsert
    {
        public String TalukaName { get; set; }
        public int CityId { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }
       
    }

    public class TalukaMasterUpdate
    {

        public int TalukaID { get; set; }
        public String TalukaName { get; set; }
        public int CityId { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

