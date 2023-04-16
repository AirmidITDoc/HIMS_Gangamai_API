using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
  public  class R_NeroSurgeryOTNotes :GenericRepository,I_NeroSurgeryOTNotes
    {
        public R_NeroSurgeryOTNotes(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string Insert(NeroSurgeryOTNotesparam NeroSurgeryOTNotesparam)
        {
            // throw new NotImplementedException();


            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OTNeroId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc = NeroSurgeryOTNotesparam.NeroSurgeryOTNotesInsert.ToDictionary();
            disc.Remove("OTNeroId");

            var Id = ExecNonQueryProcWithOutSaveChanges("insert_T_NeroSurgeryOTNotes_1", disc, outputId);

            _unitofWork.SaveChanges();
            return Id;
        }

        public bool Update(NeroSurgeryOTNotesparam NeroSurgeryOTNotesparam)
        {
            // throw new NotImplementedException();
            var disc = NeroSurgeryOTNotesparam.NeroSurgeryOTNotesUpdate.ToDictionary();
            var Id = ExecNonQueryProcWithOutSaveChanges("Update_T_NeroSurgeryOTNotes_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
