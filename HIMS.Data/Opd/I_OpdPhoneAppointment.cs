using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
    public interface I_OpdPhoneAppointment
    {
        public bool Insert(OpdPhoneAppointmentParams phoneAppointmentParams);
        public bool Cancel(OpdPhoneAppointmentParams phoneAppointmentParams);
    }
}
