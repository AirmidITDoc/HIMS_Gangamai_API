using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class PathologySampleCollectionParams
    {
        public UpdatePathologySampleCollection UpdatePathologySampleCollection { get; set; }
    }

    public class UpdatePathologySampleCollection
    {

        public int PathReportID { get; set; }
        public DateTime SampleDateTime { get; set; }
        public bool IsSampleCollection { get; set; }
       
    }
}
