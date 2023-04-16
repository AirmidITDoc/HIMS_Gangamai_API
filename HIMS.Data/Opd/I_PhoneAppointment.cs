using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public interface I_PhoneAppointment
    {
        bool Cancle(PhoneAppointmentParams PhoneAppointmentParams);
        bool Save(PhoneAppointmentParams PhoneAppointmentParams);
    }
}
