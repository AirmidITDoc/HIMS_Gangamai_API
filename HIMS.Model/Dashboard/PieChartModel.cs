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
        public string color { get; set; }
        public string extra1 { get; set; }
        public string extra2 { get; set; }
        public string extra3 { get; set; }
    }
}
