using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HIMS.Data.Opd
{
    public class R_OpdAppointment : GenericRepository, I_OpdAppointment
    {

        public R_OpdAppointment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
         
        public bool Save(OpdAppointmentParams opdAppointmentParams)
        {
            //Add registration
            var outputId = new SqlParameter
            {
              SqlDbType = SqlDbType.BigInt,
             ParameterName = "@RegId",
            Value = 0,
            Direction = ParameterDirection.Output
            };

            //Add registration
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@VisitId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var dic = opdAppointmentParams.RegistrationSave.ToDictionary();
            dic.Remove("RegId");
            var RegID=ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", dic, outputId);
            
             opdAppointmentParams.VisitSave.RegID = Convert.ToInt64(RegID);
             var dic1 = opdAppointmentParams.VisitSave.ToDictionary();
               dic1.Remove("VisitId");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("insert_VisitDetails_New_1", dic1, outputId1);

            // insert_Registration_1_1
            // var VisitID = ExecNonQueryProcWithOutSaveChanges("ps_Insert_VisitDetails", dic, outputId);
            //add SMS ,,,insert_VisitDetails_New_1
            //dic = new Dictionary<string, object>
            //{
            //    { "RegId", registerId }
            //};

            //ExecNonQueryProcWithOutSaveChanges("SMSRegistraion", dic);

            //
            //dic = new Dictionary<string, object>
            //{
            //    { "PatVisitID", Convert.ToInt64(visitId) }
            //};

            //new code
            opdAppointmentParams.TokenNumberWithDoctorWiseSave.PatVisitID = Convert.ToInt64(VisitID);
            var disc3 = opdAppointmentParams.TokenNumberWithDoctorWiseSave.ToDictionary();
           ExecNonQueryProcWithOutSaveChanges("Insert_TokenNumberWithDoctorWise",disc3);
           
            //  Insert_TokenNumberWithDoctorWise
            //commit transaction
            _unitofWork.SaveChanges();
            
            return true;
        }

        public bool Update(OpdAppointmentParams opdAppointmentParams)
        {
            //throw new NotImplementedException();

            //Update registration
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@VisitId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = opdAppointmentParams.RegistrationUpdate.ToDictionary();
            //dic.Remove("RegId");
            ExecNonQueryProcWithOutSaveChanges("update_RegForAppointment_1", dic);
            // update_RegForAppointment_1
            //add visit

          
            var Reg = opdAppointmentParams.RegistrationUpdate.RegId;
            opdAppointmentParams.VisitUpdate.RegID = Convert.ToInt64(Reg);
            var dic1 = opdAppointmentParams.VisitUpdate.ToDictionary();
            dic1.Remove("VisitId");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("insert_VisitDetails_New_1", dic1, outputId);

            // insert_Registration_1_1
            // var VisitID = ExecNonQueryProcWithOutSaveChanges("ps_Insert_VisitDetails", dic, outputId);
            //add SMS ,,,insert_VisitDetails_New_1
            //dic = new Dictionary<string, object>
            //{
            //    { "RegId", registerId }
            //};

            //ExecNonQueryProcWithOutSaveChanges("SMSRegistraion", dic);

            //
          /*  dic = new Dictionary<string, object>
            {
                { "PatVisitID", Convert.ToInt64(VisitID) }
            };*/

          opdAppointmentParams.TokenNumberWithDoctorWiseUpdate.PatVisitID = Convert.ToInt64(VisitID);
            var dic3 = opdAppointmentParams.TokenNumberWithDoctorWiseUpdate.ToDictionary();
            dic1.Remove("PatVisitID");
            ExecNonQueryProcWithOutSaveChanges("Insert_TokenNumberWithDoctorWise", dic3);
            //  Insert_TokenNumberWithDoctorWise
            //commit transaction
            _unitofWork.SaveChanges();

            return true;
        }
    }
}
