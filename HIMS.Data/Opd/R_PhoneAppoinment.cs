using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_PhoneAppoinment :GenericRepository,I_PhoneAppointment
    {
        public R_PhoneAppoinment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Cancle(PhoneAppointmentParams PhoneAppointmentParams)
        {
            var disc1 = PhoneAppointmentParams.PhoneAppointmentCancle.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_PhoneAppointment_Cancel", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(PhoneAppointmentParams PhoneAppointmentParams)
        {
            // throw new NotImplementedException();
            var disc = PhoneAppointmentParams.PhoneAppointmentInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_T_PhoneAppointment_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        

             public bool Update(PhoneAppointmentParams PhoneAppointmentParams)
        {
            // throw new NotImplementedException();
            var disc = PhoneAppointmentParams.PhoneAppointmentInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("Update_T_PhoneAppointment_Cancel", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
