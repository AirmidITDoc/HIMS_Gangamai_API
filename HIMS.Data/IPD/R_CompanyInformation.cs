using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_CompanyInformation:GenericRepository,I_CompanyInformation
    {
        public R_CompanyInformation(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(CompanyInformationparam CompanyInformationparam)
        {
            //throw new NotImplementedException();


            var disc1 = CompanyInformationparam.CompanyUpdate.ToDictionary();
            var Id=ExecNonQueryProcWithOutSaveChanges("update_CompanyInfo", disc1);


            _unitofWork.SaveChanges();
            return (true);

        }
    }
}
