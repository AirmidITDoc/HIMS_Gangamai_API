using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_AdmissionReg : GenericRepository, I_AdmissionReg
    {
        public R_AdmissionReg(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Insert(AdmissionParams AdmissionParams)
        {
          
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc2 = AdmissionParams.RegInsert.ToDictionary();
            disc2.Remove("RegID");
            
            var OutRegId = ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", disc2, outputId1);

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdmissionID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc1 = AdmissionParams.AdmissionNewInsert.ToDictionary();
            disc1.Remove("AdmissionID");
            disc1["RegId"] = OutRegId;
            var AdmissionID = ExecNonQueryProcWithOutSaveChanges("insert_Admission_1", disc1, outputId);

           // var BedId = AdmissionParams.BedUpdate.ToDictionary();

            //ExecNonQueryProcWithOutSaveChanges("ps_Update_M_BedMaster_for_Admission", BedId);

            //foreach (var a in itemMasterParams.InsertAssignItemToStore)
            //{
            //    var disc = a.ToDictionary();
            //    disc["ItemId"] = itemId;
            //    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_assignItemToStore", disc);
            //}

            _unitofWork.SaveChanges();
            return true;

         //   New Admission
//1.insert_Registration_1_1
//2.insert_Admission_1
//3.Insert_IPSMSTemplete_1 === IP Admission Msg For Patient
//4.Insert_IPSMSTemplete_1 === 'IP Admission Msg For Doctor
//5.Insert_IPSMSTemplete_1 === '  IP Admission Msg For RefDoctor
//6.UpdateQuery = "Update BedMaster set IsAvailible=0 where Bedid=" + txtBedID.Text.Trim + ""
        }

        public bool Update(AdmissionParams AdmissionParams)
        {


          /*  var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc2 = AdmissionParams.RegInsert.ToDictionary();
            disc2.Remove("RegID");

            var OutRegId = ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", disc2, outputId1);

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdmissionID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc1 = AdmissionParams.AdmissionNewInsert.ToDictionary();
            disc1.Remove("AdmissionID");
            disc1["RegId"] = OutRegId;
            var AdmissionID = ExecNonQueryProcWithOutSaveChanges("insert_Admission_1", disc1, outputId);*/
            return true;


          //  Old Admission
//1.insert_Admission_1
//2.Insert_IPSMSTemplete_1 === IP Admission Msg For Patient
//4.Insert_IPSMSTemplete_1 === 'IP Admission Msg For Doctor
//5.Insert_IPSMSTemplete_1 === '  IP Admission Msg For RefDoctor
//6.UpdateQuery = "Update BedMaster set IsAvailible=0 where Bedid=" + txtBedID.Text.Trim + ""




        }
    }

}
