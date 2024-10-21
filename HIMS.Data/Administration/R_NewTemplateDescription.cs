using HIMS.Common.Utility;
using HIMS.Model.Administration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Administration
{
   public class R_NewTemplateDescription:GenericRepository,I_NewTemplateDescription
    {
        public R_NewTemplateDescription(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork

        }

        public bool Insert(NewTemplateDescriptionParam NewTemplateDescriptionParam)
        {
            //  throw new NotImplementedException();

            var vVisitId = NewTemplateDescriptionParam.InsertTempDescParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_ReportTemplateConfig", vVisitId);

            _unitofWork.SaveChanges();

            return true;

        }

        public bool Update(NewTemplateDescriptionParam NewTemplateDescriptionParam)
        {
            var vVisitId = NewTemplateDescriptionParam.UpdateTempDescParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_ReportTemplateConfig", vVisitId);

            _unitofWork.SaveChanges();

            return true;
        }
    }
}
