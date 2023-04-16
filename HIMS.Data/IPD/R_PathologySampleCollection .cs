using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_PathologySampleCollection :GenericRepository,I_PathologySampleCollection
    {
        public R_PathologySampleCollection(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(PathologySampleCollectionParams PathologySampleCollectionParams)
        {
            // throw new NotImplementedException();

            var disc2 = PathologySampleCollectionParams.UpdatePathologySampleCollection.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_PathologySampleCollection_1", disc2);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
