using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_EmailNotification : GenericRepository, I_EmailNotification
    {
        public R_EmailNotification(IUnitofWork unitofWork) : base(unitofWork)
        {

        }


        public bool Insert(EmailNotificationParam EmailNotificationParam)
        {
            // throw new NotImplementedException();

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@Id",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = EmailNotificationParam.EmailInsert.ToDictionary();
            disc3.Remove("Id");
            var Id= ExecNonQueryProcWithOutSaveChanges("insert_EmailNotification", disc3, outputId);

            _unitofWork.SaveChanges();

            return true;
        }
    }
}