using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Dashboard
{
    public class PieChartModel
    {
        public List<string> labels { get; set; }
        public List<PieChartItem> datasets { get; set; }
    }
    public class PieChartItem
    {
        public List<decimal> data { get; set; }
        public List<string> backgroundColor { get; set; }
    }
    public class PieChartFullDto
    {
        public string Label { get; set; }
        public decimal Data { get; set; }
    }
}
