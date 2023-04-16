using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
  public  class R_SubcompanyTPA :GenericRepository,I_SubcompanyTPA
    {
        public R_SubcompanyTPA(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Insert(SubcompanyTPAparam SubcompanyTPAparam)
        {
            // throw new NotImplementedException();

            var dic = SubcompanyTPAparam.InsertsubcompanyTPA.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_M_SubTPACompanyMaster_1", dic);


            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(SubcompanyTPAparam SubcompanyTPAparam)
        {
            //throw new NotImplementedException();


            var dic = SubcompanyTPAparam.UpdatesubcompanyTPA.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_M_SubTPACompanyMaster_1", dic);


            _unitofWork.SaveChanges();
            return true;
        }
    }
}
