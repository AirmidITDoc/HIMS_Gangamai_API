using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
  public  class R_Emailconfiguration :GenericRepository,I_Emailconfiguration
    {
        public R_Emailconfiguration(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(Emailconfigurationparams Emailconfigurationparams)
        {
            //throw new NotImplementedException();
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@Id",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = Emailconfigurationparams.InsertEmailconfiguration.ToDictionary();
            disc1.Remove("Id");
            var Id = ExecNonQueryProcWithOutSaveChanges("Insert_EmailConfiguration", disc1, outputId1);


            _unitofWork.SaveChanges();
            return Id;
        }

        public bool Update(Emailconfigurationparams Emailconfigurationparams)
        {
            // throw new NotImplementedException();

            var disc2 = Emailconfigurationparams.UpdateEmailconfiguration.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_EmailConfiguration", disc2);

            _unitofWork.SaveChanges();
            return true;

        }
    }
}
