using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class RelationshipMasterParams
    {
        public RelationshipMasterInsert RelationshipMasterInsert { get; set; }
        public RelationshipMasterUpdate RelationshipMasterUpdate { get; set; }

    }

    public class RelationshipMasterInsert
    {
        public String RelationshipName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class RelationshipMasterUpdate
    {

        public int RelationshipId { get; set; }
        public String RelationshipName { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }


    }
}

