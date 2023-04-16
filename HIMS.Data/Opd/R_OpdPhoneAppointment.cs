using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_OpdPhoneAppointment:GenericRepository,I_OpdPhoneAppointment
    {
        public R_OpdPhoneAppointment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Insert(OpdPhoneAppointmentParams phoneAppointmentParams)
        {
            var disc = phoneAppointmentParams.OpdInsertPhoneAppointment.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_T_PhoneAppointment_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Cancel(OpdPhoneAppointmentParams phoneAppointmentParams)
        {
            var disc = phoneAppointmentParams.OpdCancelPhoneAppointment.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_PhoneAppointment_Cancel", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
