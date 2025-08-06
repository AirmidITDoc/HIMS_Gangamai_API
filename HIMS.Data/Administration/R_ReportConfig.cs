using HIMS.Common.Utility;
using HIMS.Model.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Administration
{
    public class R_ReportConfig : GenericRepository, I_ReportConfig
    {
        public R_ReportConfig(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool CancleReport(ReportConfigparam ReportConfigparam)
        {

            var disc1 = ReportConfigparam.Delete_ReportDetails.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Delete_ReportconfigDetails", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public String InsertReportConfig(ReportConfigparam ReportConfigparam)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ReportId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc = ReportConfigparam.InsertReportConfig.ToDictionary();
            disc.Remove("ReportId");
            var ReportId=ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ReportConfig", disc, outputId1);
            //commit transaction

            foreach (var a in ReportConfigparam.InsertReportconfigDetails)
            {
                var disc2 = a.ToDictionary();
                disc2["ReportId"] = ReportId.ToInt();
               ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ReportConfigDetails", disc2);
            }

            _unitofWork.SaveChanges();
            return ReportId;
        }

        public bool UpdateReportConfig(ReportConfigparam ReportConfigparam)
        {
            var disc = ReportConfigparam.UpdateReportConfig.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ReportConfig", disc);
            //commit transaction

            var disc1 = ReportConfigparam.Delete_ReportDetails.ToDictionary();
            disc1["ReportId"] = disc["ReportId"];
            ExecNonQueryProcWithOutSaveChanges("m_Delete_ReportconfigDetails", disc1);

            foreach (var a in ReportConfigparam.InsertReportconfigDetails)
            {
                var disc2 = a.ToDictionary();
                disc2["ReportId"] = disc["ReportId"];
                ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ReportConfigDetails", disc2);
            }

            _unitofWork.SaveChanges();
            return true;
        }


    }

       
    }

