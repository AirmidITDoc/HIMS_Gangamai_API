using HIMS.Common.Utility;
using HIMS.Model.Master;
using HIMS.Model.Master.DoctorMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Master.DoctorMaster
{
    public class R_DoctorMaster : GenericRepository,I_DoctorMaster
    {
        public R_DoctorMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(DoctorMasterParams DoctorMasterParams)
        {
            var disc1 = DoctorMasterParams.UpdateDoctorMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_DoctorMaster_1", disc1);
            

            var D_Det = DoctorMasterParams.DeleteAssignDoctorToDepartment.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Delete_AssignDoctorToDepartment", D_Det);

            
            foreach (var a in DoctorMasterParams.AssignDoctorDepartmentDet)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_DoctorDepartmentDet_1", disc);
            }
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(DoctorMasterParams DoctorMasterParams)
        {
            //add Doctor
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DoctorId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc1 = DoctorMasterParams.InsertDoctorMaster.ToDictionary();
            disc1.Remove("DoctorId");
            var doctorId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_DoctorMaster_1", disc1, outputId);

            //add DoctorDetails

            foreach (var a in DoctorMasterParams.AssignDoctorDepartmentDet)
            {
                var disc = a.ToDictionary();
                disc["DoctorId"] = doctorId;
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_DoctorDepartmentDet_1", disc);
            }

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

    }
}
