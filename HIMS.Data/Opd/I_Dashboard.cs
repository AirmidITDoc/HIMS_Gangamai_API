using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Data.Opd
{
    public interface I_Dashboard
    {
        List<dynamic> GetDashboard(Dashboard dashboard);
        //public bool GetDashboard(Dashboard dashboard);
    }
}
