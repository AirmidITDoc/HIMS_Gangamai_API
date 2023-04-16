using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_NursingTemplate:GenericRepository,I_NursingTemplate
    {
        public R_NursingTemplate(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Insert(NursingTemplateparam NursingTemplateparam)
        {
            // throw new NotImplementedException();

            var dic = NursingTemplateparam.NursingTemplateInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_M_NursingTemplateMaster_1", dic);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
