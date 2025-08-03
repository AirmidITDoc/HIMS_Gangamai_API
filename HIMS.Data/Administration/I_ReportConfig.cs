using HIMS.Model.Administration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Administration
{
    public interface I_ReportConfig
    {
        public String InsertReportConfig(ReportConfigparam ReportConfigparam);

        public bool UpdateReportConfig(ReportConfigparam ReportConfigparam);
    }
}
