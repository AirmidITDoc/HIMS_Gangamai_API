﻿using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
  public  interface I_PrescriptionTemplate
    {
        public bool Insert(Prescription_templateparam Prescription_templateparam);
    }
}
