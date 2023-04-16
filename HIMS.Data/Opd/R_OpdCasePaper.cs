using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_OpdCasePaper:GenericRepository,I_OpdCasePaper
    {
        public R_OpdCasePaper(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Insert(OpdCasePaperParams opdCasePaperParams)
        {
            var disc = opdCasePaperParams.InsertOpdCasePaper.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_T_OPDCasePaper", disc);
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(OpdCasePaperParams opdCasePaperParams)
        {
            var disc = opdCasePaperParams.UpdateOpdCasePaper.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_T_OPDCasePaper", disc);
            _unitofWork.SaveChanges();
            return true;
        }

    }
}
