using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_OTTableDetail : GenericRepository, I_OTTableDetail
    {

        public R_OTTableDetail(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool Insert(OTTableDetailParams OTTableDetailParams)
        {
            //throw new NotImplementedException();

            var disc = OTTableDetailParams.OTTableDetailInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_OTDetails_1", disc);

            _unitofWork.SaveChanges();
            return true;

        }

        public bool Update(OTTableDetailParams OTTableDetailParams)
        {
            // throw new NotImplementedException();


            var disc = OTTableDetailParams.OTTableDetailUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_OTDetails_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}