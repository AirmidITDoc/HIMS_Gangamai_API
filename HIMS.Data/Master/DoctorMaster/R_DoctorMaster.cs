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
    public class R_DoctorMaster : GenericRepository, I_DoctorMaster
    {
        public R_DoctorMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(DoctorMasterParams DoctorMasterParams)
        {
            var disc1 = DoctorMasterParams.UpdateDoctorMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_DoctorMaster_1", disc1);


            var D_Det = DoctorMasterParams.DeleteAssignDoctorToDepartment.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Delete_AssignDoctorToDepartment", D_Det);


            foreach (var a in DoctorMasterParams.AssignDoctorDepartmentDet)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Insert_M_DoctorDepartmentDet_1", disc);
            }
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(HIMS.Model.Master.DoctorMaster.DoctorMaster obj)
        {
            //add Doctor
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DoctorId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc1 = obj.ToDictionary();
            disc1.Remove("DoctorId");
            disc1.Remove("Departments");
            var doctorId = ExecNonQueryProcWithOutSaveChanges("Insert_DoctorMaster_1", disc1, outputId);

            //add DoctorDetails

            foreach (var a in obj.Departments)
            {
                var disc = a.ToDictionary();
                disc["DoctorId"] = doctorId;
                ExecNonQueryProcWithOutSaveChanges("Insert_M_DoctorDepartmentDet_1", disc);
            }

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

    }
}
