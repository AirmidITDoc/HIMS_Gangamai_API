using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class PrescriptionTemplate: GenericRepository, I_PrescriptionTemplate
    {
        public PrescriptionTemplate(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(Prescription_templateparam Prescription_templateparam)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PresId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = Prescription_templateparam.Delete_PrescriptionTemplate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Delete_M_PresTempl_1", disc1);


            var disc2 = Prescription_templateparam.Insert_TemplateH.ToDictionary();
            disc2.Remove("PresId");
            var PresId = ExecNonQueryProcWithOutSaveChanges("insert_M_PresTemplateH_1", disc2, outputId1);


            var disc3 = Prescription_templateparam.Insert_TemplateD.ToDictionary();
            disc3["PresId"] = PresId;
            ExecNonQueryProcWithOutSaveChanges("insert_M_PresTemplateD_1", disc3);


            _unitofWork.SaveChanges();
            return true;
        }
    }
}
