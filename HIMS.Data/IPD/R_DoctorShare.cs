using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_DoctorShare : GenericRepository, I_DoctorShare
    {
        public R_DoctorShare(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string Insert(Doctorshareparam Doctorshareparam)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DocShareId",
                Value = 0,
                Direction = ParameterDirection.Output
            }; 

            var disc1 = Doctorshareparam.DoctorshareheaderInsert.ToDictionary();
            disc1.Remove("DocShareId");
            var Id = ExecNonQueryProcWithOutSaveChanges("insert_DoctorShareHeader_1", disc1,outputId1);

            var dic2 = Doctorshareparam.DoctorsharemasterUpdate.ToDictionary();
            dic2["DoctorId"] = Doctorshareparam.DoctorshareheaderInsert.DoctorId;
             var Id1 = ExecNonQueryProcWithOutSaveChanges("Insert_DoctorShareMaster_1", dic2);


            _unitofWork.SaveChanges();
            return Id;
        }
    }
}
