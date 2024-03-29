﻿using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_DocumentAttachment
    {
        List<DocumentAttachmentItem> Save(List<DocumentAttachmentItem> documentAttachment);
        List<DocumentAttachmentItem> GetFiles(int OPD_IPD_ID, int OPD_IPD_Type);
        DocumentAttachmentItem GetFileById(int Id);
    }
}
