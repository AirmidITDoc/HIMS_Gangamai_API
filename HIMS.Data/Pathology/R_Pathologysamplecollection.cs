using HIMS.Common.Utility;
using HIMS.Model.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pathology
{
   public class R_Pathologysamplecollection :GenericRepository,I_Pathologysamplecollection
    {
        public R_Pathologysamplecollection(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Update(Pathologysamplecollectionparameter Pathologysamplecollectionparameter)
        {
            // throw new NotImplementedException();
            foreach (var a in Pathologysamplecollectionparameter.Updatepathologysamplecollection)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_PathologySampleCollection_1", disc);
            }
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
