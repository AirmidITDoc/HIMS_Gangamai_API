using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_OPDPrescription :GenericRepository,I_OPDPrescription
    {
        public R_OPDPrescription(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }


        public bool Insert(OPDPrescriptionParams OPDPrescriptionParams)
        {
            // throw new NotImplementedException();
          
            var dic = OPDPrescriptionParams.UpdateOPDPrescription.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_T_Prescription", dic);


            var dic1 = OPDPrescriptionParams.InsertOPDPrescription.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_Prescription_1", dic1);

            _unitofWork.SaveChanges();

            return true;
        }
    }
}
