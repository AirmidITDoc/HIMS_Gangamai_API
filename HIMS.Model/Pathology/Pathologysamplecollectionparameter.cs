using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pathology
{
    public class Pathologysamplecollectionparameter
    {
        public List<Updatepathologysamplecollection> Updatepathologysamplecollection { get; set; }
    }

    public class Updatepathologysamplecollection
    {
        public int PathReportID { get; set; }
        public DateTime SampleDateTime { get; set; }
        public bool IsSampleCollection { get; set; }
        public int No { get; set; }

    }
}

