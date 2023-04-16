using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_DoctorNote : GenericRepository, I_DoctorNote
    {

        public R_DoctorNote(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }
       

        public String Insert(DoctorNoteparam DoctorNoteparam)
        {
            // throw new NotImplementedException();


            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DoctNoteId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = DoctorNoteparam.DoctorNoteInsert.ToDictionary();
            dic.Remove("DoctNoteId");
            var Id=ExecNonQueryProcWithOutSaveChanges("insert_T_Doctors_Notes_1", dic,outputId);

            _unitofWork.SaveChanges();
            return Id;
        }
    }
}
