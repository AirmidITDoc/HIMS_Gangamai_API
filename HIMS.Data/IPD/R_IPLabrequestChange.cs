using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPLabrequestChange :GenericRepository,I_IPLabrequestChange
    {
        public R_IPLabrequestChange(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Insert(IPLabrequestChangeParam IPLabrequestChangeParam)
        {
           // throw new NotImplementedException();
            var dic = IPLabrequestChangeParam.InsertIPRequestLabcharges.ToDictionary();
            dic.Remove("RequestId");
            ExecNonQueryProcWithOutSaveChanges("Insert_LabRequest_Charges_1", dic);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
