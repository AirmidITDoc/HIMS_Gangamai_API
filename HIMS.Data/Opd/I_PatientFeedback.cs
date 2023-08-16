using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Data.Opd
{
    public interface I_PatientFeedback
    {
        public bool Insert(PatientFeedbackParams PatientFeedbackParams);
    }
}
