using HIMS.Common.Utility;
using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
   public    class R_NewMenuMaster:GenericRepository,I_NewMenuMaster
    {
        public R_NewMenuMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Save(NewMenumasterParam NewMenumasterParam)
        {
            //  throw new NotImplementedException();

            var disc = NewMenumasterParam.MenuInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_MenuMaster_New", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(NewMenumasterParam NewMenumasterParam)
        {
            // throw new NotImplementedException();
            var disc = NewMenumasterParam.MenuUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_MenuMaster_New", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
