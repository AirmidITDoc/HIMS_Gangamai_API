using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
  public  class R_ProjectInformation :GenericRepository,I_ProjectInformation
    {
        public R_ProjectInformation(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Save(ProjectInformationParams ProjectInformationParams)
        {
            //throw new NotImplementedException();
            var disc1 = ProjectInformationParams.ProjectInformationInsert .ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_T_ProjectInformation", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(ProjectInformationParams ProjectInformationParams)
        {

            var disc1 = ProjectInformationParams.ProjectInformationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_ProjectInformation", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

       
    }

}
