using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Dashboard
{
    public class PieChartModel
    {
        public List<PieChartDto> data { get; set; }
        public List<string> colors { get; set; }
    }
    public class PieChartDto
    {
        public string name { get; set; }
        public string value { get; set; }

    }
}
