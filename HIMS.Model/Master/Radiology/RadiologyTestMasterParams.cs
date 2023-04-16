using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Radiology
{
    public class RadiologyTestMasterParams
    {
        public InsertRadiologyTestMaster InsertRadiologyTestMaster { get; set; }
        public UpdateRadiologyTestMaster UpdateRadiologyTestMaster { get; set; }
        public List<InsertRadiologyTemplateTest> InsertRadiologyTemplateTest { get; set; }
        public RadiologyTemplateDetDelete RadiologyTemplateDetDelete { get; set; }
    }
    public class InsertRadiologyTestMaster
    {
        public string TestName { get; set; }
        public string PrintTestName { get; set; }
        public long CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
        public long ServiceId { get; set; }
    }
    public class UpdateRadiologyTestMaster
    {
        public long TestId { get; set; }
        public string TestName { get; set; }
        public string PrintTestName { get; set; }
        public long CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
        public long ServiceId { get; set; }
    }

    public class InsertRadiologyTemplateTest
    {
        public long TestId { get; set; }
        public long TemplateId { get; set; }
    }
    public class RadiologyTemplateDetDelete
    {
        public long TestId { get; set; }
    }
}
