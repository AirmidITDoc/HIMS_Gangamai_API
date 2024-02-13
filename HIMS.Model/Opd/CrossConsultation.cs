using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
   public class CrossConsultation
    {
        public CrossConsultationSave CrossConsultationSave { get; set; }
    }


    public class CrossConsultationSave
    {
        public long VisitId { get; set; }
        public long RegID { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime VisitTime { get; set; }
        public long UnitId { get; set; }
        public long PatientTypeId { get; set; }
        public long ConsultantDocId { get; set; }
        public long RefDocId { get; set; }
        public long TariffId { get; set; }
        public long CompanyId { get; set; }
        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }

        public bool IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public long ClassId { get; set; }
        public long DepartmentId { get; set; }
        public long PatientOldNew { get; set; }
        public int FirstFollowupVisit { get; set; }
        public long AppPurposeId { get; set; }
        public DateTime FollowupDate { get; set; }

        public int CrossConsulFlag { get; set; }
        // public Boolean IsXray { get; set; }
    }
}
