﻿using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Pharmacy
{
    public  class R_MaterialAcceptance : GenericRepository, I_MaterialAcceptance
    {
        public R_MaterialAcceptance(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool UpdateMaterialAcceptance(MaterialAcceptParams materialAcceptParams)
        {
            var vMaterialAcceptUdpate = materialAcceptParams.MaterialAcceptIssueHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_AcceptMaterial_Store_1", vMaterialAcceptUdpate);
            
            foreach (var a in materialAcceptParams.MaterialAcceptIssueDetails)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_update_AcceptMaterialIssueDet_1", disc1);
            }
            _unitofWork.SaveChanges();
            return true;
        }
    }
}