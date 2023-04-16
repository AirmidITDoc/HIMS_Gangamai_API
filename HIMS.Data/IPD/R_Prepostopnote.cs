using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_Prepostopnote : GenericRepository,I_Prepostopnote
    {
        public R_Prepostopnote(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(Prepostopnoteparam Prepostopnoteparam)
        {
            //throw new NotImplementedException();
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OTId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc = Prepostopnoteparam.InsertPrepostopnote.ToDictionary();
            disc.Remove("OTId");

            var Id = ExecNonQueryProcWithOutSaveChanges("insert_T_PrePostOperativeNotes_1", disc, outputId);

            _unitofWork.SaveChanges();
            return Id;
        }

        public bool Update(Prepostopnoteparam Prepostopnoteparam)
        {
            // throw new NotImplementedException();

            var disc = Prepostopnoteparam.UpdatePrepostopnote.ToDictionary();
            
            var Id = ExecNonQueryProcWithOutSaveChanges("update_T_PrePostOperativeNotes_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
