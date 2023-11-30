using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_DocumentAttachment
    {
        List<DocumentAttachmentItem> Save(List<DocumentAttachmentItem> documentAttachment);
    }
}
