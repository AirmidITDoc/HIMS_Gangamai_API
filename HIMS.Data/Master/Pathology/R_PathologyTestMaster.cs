﻿using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public class R_PathologyTestMaster:GenericRepository,I_PathologyTestMaster
    {
        public R_PathologyTestMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(PathologyTestMasterParams pathTestMasterParams)
        {
            //Update PathologyTest
            var dic1 = pathTestMasterParams.UpdatePathologyTestMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_PathologyTestMaster_1", dic1);


            if (pathTestMasterParams.UpdatePathologyTestMaster.IsTemplateTest == true)
            {
                var S_Det1 = pathTestMasterParams.PathTemplateDetDelete.ToDictionary();
                S_Det1["TestId"] = pathTestMasterParams.UpdatePathologyTestMaster.TestId;
                ExecNonQueryProcWithOutSaveChanges("m_Delete_M_PathTemplateDetails", S_Det1);

                foreach (var a in pathTestMasterParams.PathologyTemplateTest)
                {
                    var d = a.ToDictionary();
                    d["TestId"]= pathTestMasterParams.UpdatePathologyTestMaster.TestId;
                    ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyTemplateTest_1", d);
                }

            }
            else
            {
                // DeletePathTestDetail
                var S_Det = pathTestMasterParams.PathTestDetDelete.ToDictionary();
                S_Det["TestId"] = pathTestMasterParams.UpdatePathologyTestMaster.TestId;
                ExecNonQueryProcWithOutSaveChanges("m_Delete_M_PathTestDetailMaster", S_Det);

                foreach (var a in pathTestMasterParams.PathTestDetailMaster)
                {
                    var d = a.ToDictionary();
                    d["TestId"] = pathTestMasterParams.UpdatePathologyTestMaster.TestId;
                    ExecNonQueryProcWithOutSaveChanges("m_insert_PathTestDetailMaster_1", d);
                }

            }
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(PathologyTestMasterParams pathTestMasterParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@TestId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = pathTestMasterParams.InsertPathologyTestMaster.ToDictionary();
            dic.Remove("TestId");
            var TestId = ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyTestMaster_1", dic, outputId);

            if (pathTestMasterParams.InsertPathologyTestMaster.IsTemplateTest == true)
            {
                foreach (var a in pathTestMasterParams.PathologyTemplateTest)
                {
                    var d = a.ToDictionary();
                    d["TestId"] = TestId;
                    ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyTemplateTest_1", d);
                }
            }
            else
            {
                foreach (var a in pathTestMasterParams.PathTestDetailMaster)
                {
                    var d = a.ToDictionary();
                    d["TestId"] = TestId;
                    ExecNonQueryProcWithOutSaveChanges("m_insert_PathTestDetailMaster_1", d);
                }
            }

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
