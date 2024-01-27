using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Dashboard
{
    public class BarChartModel
    {
        public List<BarChartDto> data { get; set; }
        public List<string> colors { get; set; }
    }
    public class BarChartDto
    {
        public string name { get; set; }
        public List<BarChartItem> series { get; set; }
    }
    public class BarChartItem
    {
        public string name { get; set; }
        public decimal value { get; set; }
        public string multi { get; set; }
        public long multiid { get; set; }
    }
}
