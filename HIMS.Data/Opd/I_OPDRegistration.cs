﻿using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Data.Opd
{
   public interface I_OPDRegistration
    {
        string Insert(OPDRegistrationParams OPDRegistrationParams);
        bool Update(OPDRegistrationParams OPDRegistrationParams);
    }
}
