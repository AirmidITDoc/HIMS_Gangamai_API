using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class Prescription_templateparam
    {
        public Delete_PrescriptionTemplate Delete_PrescriptionTemplate { get; set; }
        public Insert_TemplateH Insert_TemplateH { get; set; }
        public List<Insert_TemplateD> Insert_TemplateD { get; set; }
    }

    public class Delete_PrescriptionTemplate
    {
        public int PresId { get; set; }
    }

    public class Insert_TemplateH
    {

        public int PresId { get; set; }
        public string PresTemplateName { get; set; }
        public int IsAddBy { get; set; }
        public int IsDeleted { get; set; }
        public int OP_IP_Type { get; set; }

    }

    public class Insert_TemplateD
    {


        public int PresId { get; set; }

        public DateTime Date { get; set; }
        public int ClassID { get; set; }
        public int GenericId { get; set; }
        public int DrugId { get; set; }
        public int DoseId { get; set; }
        public int Days { get; set; }
        public int InstructionId { get; set; }
        public float QtyPerDay { get; set; }

        public int TotalQty { get; set; }
        public string Instruction { get; set; }
        public string Remark { get; set; }
        public bool IsEnglishOrIsMarathi { get; set; }

    }
}
