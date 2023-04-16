using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
    public interface I_EmailNotification
    {
        bool Insert(EmailNotificationParam EmailNotificationParam);
    }
}
