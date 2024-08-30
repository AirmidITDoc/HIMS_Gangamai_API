using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
  public  class R_BedTransfer :GenericRepository,I_BedTransfer
    {
        public readonly SqlCommand command;

        public R_BedTransfer(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

         public bool Update(BedTransferParams BedTransferParams)
        {
            
               // throw new NotImplementedException();

               var dic = BedTransferParams.UpdateBedtransferSetFix.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_BedMaster", dic);
            
            var disc3 = BedTransferParams.UpdateBedtransferSetFree.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_BedMasterTofreebed", disc3);

            BedTransferParams.UpdateAdmissionBedtransfer.BedId = BedTransferParams.UpdateBedtransferSetFix.BedId;
            var disc2 = BedTransferParams.UpdateAdmissionBedtransfer.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_AdmissionforBedMaster", disc2);

                        
            BedTransferParams.UpdateBedtransfer.AdmissionID = BedTransferParams.UpdateAdmissionBedtransfer.AdmissionID;
            BedTransferParams.UpdateBedtransfer.ToWardID = BedTransferParams.UpdateAdmissionBedtransfer.WardId;
            BedTransferParams.UpdateBedtransfer.ToBedId = BedTransferParams.UpdateAdmissionBedtransfer.BedId;
            BedTransferParams.UpdateBedtransfer.ToClassId = BedTransferParams.UpdateAdmissionBedtransfer.ClassId;

            var disc4 = BedTransferParams.UpdateBedtransfer.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_BedTransferDetails_1", disc4);

            _unitofWork.SaveChanges();
            return true;
        }

    }
}
