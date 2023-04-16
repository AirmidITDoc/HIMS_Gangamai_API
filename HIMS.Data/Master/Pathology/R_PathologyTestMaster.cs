using HIMS.Common.Utility;
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
            //var dic2 = pathTestMasterParams.updatePathologyTemplateTest.ToDictionary();
           // ExecNonQueryProcWithOutSaveChanges("ps_Update_M_PathologyTemplateTest", dic2);
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_PathologyTestMaster", dic1);


            // DeletePathTestDetail
            var S_Det = pathTestMasterParams.PathTestDetDelete.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("p_Delete_M_PathTestDetailMaster", S_Det);
            var S_Det1 = pathTestMasterParams.PathTemplateDetDelete.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("p_Delete_M_PathTemplateDetails", S_Det1);

            if (pathTestMasterParams.UpdatePathologyTestMaster.IsTemplateTest == true)
            {
                foreach (var a in pathTestMasterParams.PathologyTemplateTest)
                {
                    var d = a.ToDictionary();
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathologyTemplateTestMaster", d);
                }

            }
            else
            {
                foreach (var a in pathTestMasterParams.PathTestDetailMaster)
                {
                    var d = a.ToDictionary();
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathTestDetailMaster", d);
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
            var TestId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathologyTestMaster", dic, outputId);

            if (pathTestMasterParams.InsertPathologyTestMaster.IsTemplateTest == true)
            {
                foreach (var a in pathTestMasterParams.PathologyTemplateTest)
                {
                    var d = a.ToDictionary();
                    d["TestId"] = TestId;
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathologyTemplateTestMaster", d);
                }

            }
            else
            {
                foreach (var a in pathTestMasterParams.PathTestDetailMaster)
                {
                    var d = a.ToDictionary();
                    d["TestId"] = TestId;
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathTestDetailMaster", d);
                }

            }


           _unitofWork.SaveChanges();
            return true;
        }
    }
}
