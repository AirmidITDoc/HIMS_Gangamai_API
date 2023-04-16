using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_RegisteredPatientAdmission : GenericRepository, I_RegisteredPatientAdmission
    {
        public R_RegisteredPatientAdmission(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

            public bool Update(RegisteredPatientAdmissionParams RegisteredPatientAdmissionParams)
           {

               var outputId = new SqlParameter
               {
                   SqlDbType = SqlDbType.BigInt,
                   ParameterName = "@AdmissionID",
                   Value = 0,
                   Direction = ParameterDirection.Output
               };


               
               var disc1 = RegisteredPatientAdmissionParams.AdmissionInsert.ToDictionary();
               disc1.Remove("AdmissionID");
               var AdmissionID = ExecNonQueryProcWithOutSaveChanges("insert_Admission_1", disc1, outputId);

               // var BedId = AdmissionParams.BedUpdate.ToDictionary();
               // ExecNonQueryProcWithOutSaveChanges("ps_Update_M_BedMaster_for_Admission", BedId);

               //foreach (var a in itemMasterParams.InsertAssignItemToStore)
               //{
               //    var disc = a.ToDictionary();
               //    disc["ItemId"] = itemId;
               //    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_assignItemToStore", disc);
               //}

               _unitofWork.SaveChanges();
               return true;
           }

        //public bool Update(AdmissionParams AdmissionParams)
        //{

        //    return true;
        //}
    }
}
