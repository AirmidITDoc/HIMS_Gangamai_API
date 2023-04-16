using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_Admission : GenericRepository, I_Admission
    {
        public R_Admission(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public String Insert(AdmissionParams AdmissionParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdmissionID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc2 = AdmissionParams.RegInsert.ToDictionary();
            disc2.Remove("RegId");
            var RegId = ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", disc2, outputId1);

            AdmissionParams.AdmissionNewInsert.RegId = Convert.ToInt32(RegId);
            var disc1 = AdmissionParams.AdmissionNewInsert.ToDictionary();
            disc1.Remove("AdmissionID");
            var AdmissionID = ExecNonQueryProcWithOutSaveChanges("insert_Admission_1", disc1, outputId);

            //BedUpdate
         //  var BedId = AdmissionParams.BedUpdate.ToDictionary();
           //ExecNonQueryProcWithOutSaveChanges("ps_Update_M_BedMaster_for_Admission", BedId);

            //IpSMSTemplate
           // var disc3 = AdmissionParams.IpSMSTemplateInsert.ToDictionary();
          //  disc2.Remove("RegId");
           //ExecNonQueryProcWithOutSaveChanges("Insert_IPSMSTemplete_1", disc3);

            //foreach (var a in itemMasterParams.InsertAssignItemToStore)
            //{
            //    var disc = a.ToDictionary();
            //    disc["ItemId"] = itemId;
            //    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_assignItemToStore", disc);
            //}



            _unitofWork.SaveChanges();
            return (AdmissionID);
        }

        public bool Update(AdmissionParams AdmissionParams)
        {
            // throw new NotImplementedException();

            var dic = AdmissionParams.AdmissionNewUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Admission_1", dic);

            _unitofWork.SaveChanges();
            return true;


        }

        
    }
}
