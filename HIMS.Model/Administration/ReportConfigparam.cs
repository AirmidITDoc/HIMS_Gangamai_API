using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Administration
{
    public class ReportConfigparam
    {

        public InsertReportConfig InsertReportConfig { get; set; }
        public List<InsertReportconfigDetails> InsertReportconfigDetails { get; set; }

        public UpdateReportConfig UpdateReportConfig { get; set; }
        //public List<UpdateReportconfigDetails> UpdateReportconfigDetails { get; set; }

    }


   public class InsertReportConfig { 
        public long ReportId { get; set; }
        public string ReportSection { get; set; }
        public string ReportName { get; set; }
        public int Parentid { get; set; }
       
        public string ReportMode { get; set; }

        public string ReportTitle { get; set; }
        public string ReportHeader { get; set; }
        public string ReportColumn { get; set; }
        public string ReportTotalField { get; set; }
        public string ReportGroupByLabel { get; set; }
        public string summaryLabel { get; set; }
        public string ReportHeaderFile { get; set; }
        public string ReportBodyFile { get; set; }
        public string ReportFolderName { get; set; }
        public string ReportFileName { get; set; }
        public string ReportSPName { get; set; }
        public string ReportPageOrientation { get; set; }
        public string ReportPageSize { get; set; }
        public string ReportColumnWidths { get; set; }
        public string ReportFilter { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }

       // public DateTime CreatedOn { get; set; }
        public long UpdateBy { get; set; }
       // public DateTime UpdatedOn { get; set; }
        public long MenuId { get; set; }

    }

    public class InsertReportconfigDetails
    {
        public int ReportColId { get; set; }
        public int ReportId { get; set; }
        public bool IsDisplayColumn { get; set; }
        public string ReportHeader { get; set; }
        public string ReportColumn { get; set; }
        public string ReportColumnWidth { get; set; }
        public string ReportColumnAligment { get; set; }
        public string ReportTotalField { get; set; }

        public string ReportGroupByLabel { get; set; }
        public string SummaryLabel { get; set; }
        public int SequenceNo { get; set; }
        public string ProcedureName { get; set; }
    }


    
         public class UpdateReportConfig
    {
        public long ReportId { get; set; }
        public string ReportSection { get; set; }
        public string ReportName { get; set; }
        public int Parentid { get; set; }

        public string ReportMode { get; set; }

        public string ReportTitle { get; set; }
        public string ReportHeader { get; set; }
        public string ReportColumn { get; set; }
        public string ReportTotalField { get; set; }
        public string ReportGroupByLabel { get; set; }
        public string summaryLabel { get; set; }
        public string ReportHeaderFile { get; set; }
        public string ReportBodyFile { get; set; }
        public string ReportFolderName { get; set; }
        public string ReportFileName { get; set; }
        public string ReportSPName { get; set; }
        public string ReportPageOrientation { get; set; }
        public string ReportPageSize { get; set; }
        public string ReportColumnWidths { get; set; }
        public string ReportFilter { get; set; }
        public bool IsActive { get; set; }
        //public long CreatedBy { get; set; }

        // public DateTime CreatedOn { get; set; }
        public long UpdateBy { get; set; }
        // public DateTime UpdatedOn { get; set; }
        public long MenuId { get; set; }

    }

}
