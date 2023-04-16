using HIMS.Common.Utility;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Prescription
{
   public class R_PrescriptionTemplateMaster :GenericRepository,I_PrescriptionTemplateMaster
    {
        public R_PrescriptionTemplateMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(PrescriptionTemplateMasterParams PrescriptionTemplateMasterParams)
        {
            var disc1 = PrescriptionTemplateMasterParams.PrescriptionTemplateMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_Prescription_TemplateMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

         public bool Insert(PrescriptionTemplateMasterParams PrescriptionTemplateMasterParams)
        {
            //throw new NotImplementedException();
            var disc = PrescriptionTemplateMasterParams.PrescriptionTemplateMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_Prescription_TemplateMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

       
    }
}
