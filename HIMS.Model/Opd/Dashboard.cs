using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{

    public class Dashboard
    {
        public widget4 widget4 { get; set; }
    }
    
    public class widget4
    {
        public string title { get; set; }
        public string extra { get; set; }
        public List<data1> data1 { get; set; }

        //public DateTime VisitDate { get; set; }
    }
    public class data1
    {
        public string Label { get; set; }
        public string Count { get; set; }
    }
}
