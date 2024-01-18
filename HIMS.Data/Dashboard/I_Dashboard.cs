using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Dashboard;
using HIMS.Model.Master;
using HIMS.Model.Users;

namespace HIMS.Data.Dashboard
{
    public interface I_Dashboard
    {
        public PieChartModel GetPieChartData(string procName, Dictionary<string, object> entity);
    }
}
