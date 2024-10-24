﻿using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_StoreMaster:GenericRepository,I_StoreMaster
    {

        public R_StoreMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(StoreMasterParams storeMasterParams)
        {
            var disc = storeMasterParams.UpdateStoreMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("M_Update_StoreMaster1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(StoreMasterParams storeMasterParams)
        {
            var disc = storeMasterParams.InsertStoreMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_StoreMster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        //public bool Update(StoreMasterParams storeMasterParams)
        //{
        //    var disc = storeMasterParams.InsertStoreMaster.ToDictionary();
        //    ExecNonQueryProcWithOutSaveChanges("M_Update_StoreMaster]", disc);
        //    _unitofWork.SaveChanges();
        //    return true;
        //}

    }
}
