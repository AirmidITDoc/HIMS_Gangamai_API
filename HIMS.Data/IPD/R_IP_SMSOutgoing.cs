using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
  public  class R_IP_SMSOutgoing :GenericRepository,I_IP_SMSOutgoing
    {
        public R_IP_SMSOutgoing(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
        public bool Insert(IPSMSOutgoingparams IPSMSOutgoingparams)
        {
            // throw new NotImplementedException();

                var disc = IPSMSOutgoingparams.IPSMSOutgoingInsert.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_SMSOutGoing_1", disc);
          
            _unitofWork.SaveChanges();
            return true;
        }

    }
}
