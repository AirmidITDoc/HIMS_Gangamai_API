using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Pathology
{
    public class PathologyTestMasterParams
    {
        public InsertPathologyTestMaster InsertPathologyTestMaster { get; set; }
        public UpdatePathologyTestMaster UpdatePathologyTestMaster { get; set; }

        public UpdatePathologyTemplateTest updatePathologyTemplateTest { get; set; }
        public List<PathTestDetailMaster> PathTestDetailMaster { get; set; }
        public List<PathologyTemplateTest> PathologyTemplateTest { get; set; }
        public PathTestDetDelete PathTestDetDelete { get; set; }
        public PathTemplateDetDelete PathTemplateDetDelete { get; set; }

    }
        public class InsertPathologyTestMaster
        { 
        public string TestName { get; set; }
        public string PrintTestName { get; set; }
        public long CategoryId { get; set; }
        public bool IsSubTest { get; set; }
        public string TechniqueName { get; set; }
        public string MachineName { get; set; }
        public string SuggestionNote { get; set; }
        public string FootNote { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
        public long ServiceId { get; set; }
        public bool IsTemplateTest { get; set; }
        public bool IsCategoryPrint { get; set; }
        public bool IsPrintTestName { get; set; }
 
    }
    public class UpdatePathologyTestMaster
    {
        public long TestId { get; set; }
        public string TestName { get; set; }
        public string PrintTestName { get; set; }
        public long CategoryId { get; set; }
        public bool IsSubTest { get; set; }
        public string TechniqueName { get; set; }
        public string MachineName { get; set; }
        public string SuggestionNote { get; set; }
        public string FootNote { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
        public long ServiceId { get; set; }
        public bool IsTemplateTest { get; set; }
        public bool IsCategoryPrint { get; set; }
        public bool IsPrintTestName { get; set; }
       

    }

    public class UpdatePathologyTemplateTest
    {
        public long TestId { get; set; }
        public long TemplateId { get; set; }


    }
    public class PathTestDetailMaster
    {
        
        public long TestId { get; set; }
        public long SubTestID { get; set; }
        public long ParameterID { get; set; }
    }
    public class PathTestDetDelete
    {
        public long TestId { get; set; }
    }
    public class PathTemplateDetDelete
    {
        public long TestId { get; set; }
    }
    public class PathologyTemplateTest
    {
        public long TestId { get; set; }
        public long TemplateId { get; set; }
        

    }
}
