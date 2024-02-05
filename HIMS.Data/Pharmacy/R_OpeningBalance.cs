using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_OpeningBalance:GenericRepository,I_Openingbalance
    {
        public R_OpeningBalance(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Insert(Openingbalance Openingbalance)
        {
            //throw new NotImplementedException();

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OpeningHId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OpeningId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc1 = Openingbalance.InsertOpeningbalanceheader.ToDictionary();
            disc1.Remove("OpeningHId");
            var Id = ExecNonQueryProcWithOutSaveChanges("Insert_OpeningTransaction_header_1", disc1, outputId);

            foreach (var a in Openingbalance.InsertOpeningbalancedetail) {
                var disc2 = a.ToDictionary();
                disc2["StoreId"] = Openingbalance.InsertOpeningbalanceheader.StoreId;
                disc2.Remove("OpeningId");
                var Id1 = ExecNonQueryProcWithOutSaveChanges("Insert_OpeningTransaction_1", disc2, outputId1);
            }
            _unitofWork.SaveChanges();
            return true;

        }

        public bool Update(Openingbalance Openingbalance)
        {
            //  throw new NotImplementedException();

            var disc2 = Openingbalance.InsertOpeningbalanceheader.ToDictionary();
          //  disc2.Remove("OpeningId");
            var Id1 = ExecNonQueryProcWithOutSaveChanges("Insert_OpeningTransaction_1", disc2);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
