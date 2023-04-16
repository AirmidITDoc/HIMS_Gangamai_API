using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_CasePaperPrescription : GenericRepository,I_CasePaperPrescription
    {
        public R_CasePaperPrescription(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public String Insert(CasePaperPrescriptionParams CasePaperPrescriptionParams)
        {
       // F:\AirmidHIMS\CNS_API\HIMS.sln
        //  throw new NotImplementedException();
        //Add registration
        var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@CasePaperID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc = CasePaperPrescriptionParams.InsertOpCasePaper.ToDictionary();
            disc.Remove("CasePaperID");
            var CasePaperID=ExecNonQueryProcWithOutSaveChanges("Insert_T_OPDCasePaper_1", disc,outputId);
            
            foreach (var a in CasePaperPrescriptionParams.InsertOPPrescription)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_Prescription_1", disc1);
            }

            _unitofWork.SaveChanges();
            return(CasePaperID);
        }

       

    }
}
