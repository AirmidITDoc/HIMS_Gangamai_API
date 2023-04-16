using HIMS.Common.Utility;
using HIMS.Model.Master.Radiology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Master.Radiology
{
    public class R_RadiologyTestMaster : GenericRepository, I_RadiologyTestMaster
    {
        public R_RadiologyTestMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(RadiologyTestMasterParams rtMasterParams)
        {
            var disc1 = rtMasterParams.UpdateRadiologyTestMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_RadiologyTestMaster", disc1);

            var D_Det = rtMasterParams.RadiologyTemplateDetDelete.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("p_Delete_M_RadiologyTemplateDetails", D_Det);

            foreach (var a in rtMasterParams.InsertRadiologyTemplateTest)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_RadiologyTemplateTest", disc);
            }
            //-----------------------


            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(RadiologyTestMasterParams rtMasterParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@TestId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc1 = rtMasterParams.InsertRadiologyTestMaster.ToDictionary();
            disc1.Remove("TestId");
            var testId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_Radiology_TestMaster", disc1, outputId);

            //add DoctorDetails

            foreach (var a in rtMasterParams.InsertRadiologyTemplateTest)
            {
                var disc = a.ToDictionary();
                disc["TestId"] = testId;
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_RadiologyTemplateTest", disc);
            }

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
