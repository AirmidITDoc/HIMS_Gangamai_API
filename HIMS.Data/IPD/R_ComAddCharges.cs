using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_ComAddCharges : GenericRepository, I_ComAddcharges
    {
        public R_ComAddCharges(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

       
       
        public bool Save(ComAddChargesParams comaddChargesParams)
        {
            // add AddCharges
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ChargeID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = comaddChargesParams.ComAddCharges.ToDictionary();
            dic.Remove("ChargeID");
            var ChargesId = ExecNonQueryProcWithOutSaveChanges("insert_IPAddCharges_Com_1", dic, outputId);


          

           
            _unitofWork.SaveChanges();

            return true;
        }
        public bool update(ComAddChargesParams comaddChargesParams)
        {
            //throw new NotImplementedException();


            var disc = comaddChargesParams.UpdateCharges.ToDictionary();
            var Id = ExecNonQueryProcWithOutSaveChanges("insert_IPAddCharges_Com_1", disc);


            _unitofWork.SaveChanges();
            return (true);

        }



        //public bool Save(CompanyAddParams companyAddParams)
        //{
        //    // add AddCharges
        //    var outputId = new SqlParameter
        //    {
        //        SqlDbType = SqlDbType.BigInt,
        //        ParameterName = "@AdmId",
        //        Value = 0,
        //        Direction = ParameterDirection.Output
        //    };
        //    var dic = companyAddParams.CompanyAdd.ToDictionary();
        //    dic.Remove("AdmId");
        //    var AdmId = ExecNonQueryProcWithOutSaveChanges("Upt_Comp_Prc_HBil", dic, outputId);





        //    _unitofWork.SaveChanges();

        //    return true;
        //}

        public bool update(CompanyAddParams companyAddParams)
        {
            //throw new NotImplementedException();


            var disc = companyAddParams.Companyupdate.ToDictionary();
            var Id = ExecNonQueryProcWithOutSaveChanges("Upt_Comp_Prc_HBil", disc);


            _unitofWork.SaveChanges();
            return (true);

        }
    }
}
