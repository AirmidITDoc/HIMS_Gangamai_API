﻿using HIMS.Model.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pathology
{
   public interface I_pathresultentry
    {
        public bool Insert(pathresultentryparam pathresultentryparam);

        string ViewPathTestReport(int OP_IP_Type, string htmlFilePath, string HeaderName);
    }
}
