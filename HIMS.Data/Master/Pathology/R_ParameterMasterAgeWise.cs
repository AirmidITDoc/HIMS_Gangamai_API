using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public class R_ParameterMasterAgeWise:GenericRepository,I_ParameterMasterAgeWise
    {
        public R_ParameterMasterAgeWise(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }
        public bool Insert(PathParameterMasterParams pathParameterMasterParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ParameterID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = pathParameterMasterParams.PathParameterMasterInsert.ToDictionary();
            disc1.Remove("ParameterID");
            var ParameterID = ExecNonQueryProcWithOutSaveChanges("m_Insert_PathParameterMaster_1", disc1, outputId);

            //add Range and Descriptive
            if (pathParameterMasterParams.PathParameterMasterInsert.IsNumeric == 1)
            {
                foreach (var a in pathParameterMasterParams.ParameterRangeWithAgeMasterInsert)
                {
                    var disc = a.ToDictionary();
                    disc["ParaId"] = ParameterID;
                    ExecNonQueryProcWithOutSaveChanges("m_Insert_ParameterRangeWithAgeMaster_1", disc);
                }
            }
            else
            {
                foreach (var a in pathParameterMasterParams.ParameterDescriptiveMasterInsert)
                {
                    var disc = a.ToDictionary();
                    disc["ParameterID"] = ParameterID;
                    ExecNonQueryProcWithOutSaveChanges("m_Insert_ParameterDescriptiveMaster_1", disc);
                }
            }
            _unitofWork.SaveChanges();
            return true;
        }


        public bool Update(PathParameterMasterParams pathParameterMasterParams)
        {
            var disc1 = pathParameterMasterParams.PathParameterMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_ParameterMaster_2", disc1);


            if (pathParameterMasterParams.PathParameterMasterUpdate.IsNumeric == 1)
            {

                var D_Det = pathParameterMasterParams.ParameterRangeWithAgeMasterDelete.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Delete_RangeParameterWithAgeMaster_1", D_Det);

                foreach (var a in pathParameterMasterParams.ParameterRangeWithAgeMasterInsert)
                {
                    var disc = a.ToDictionary();
                    disc["ParaId"] = pathParameterMasterParams.PathParameterMasterUpdate.ParameterID;
                    ExecNonQueryProcWithOutSaveChanges("m_Insert_ParameterRangeWithAgeMaster_1", disc);
                }


            }
            else
            {

                var D_Det = pathParameterMasterParams.DescriptiveParameterMasterDelete.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Delete_DescriptiveParameterMaster_1", D_Det);

                foreach (var a in pathParameterMasterParams.ParameterDescriptiveMasterInsert)
                {
                    var disc = a.ToDictionary();
                    disc["ParameterID"] = pathParameterMasterParams.PathParameterMasterUpdate.ParameterID; 
                    ExecNonQueryProcWithOutSaveChanges("m_Insert_ParameterDescriptiveMaster_1", disc);
                }
            }

            _unitofWork.SaveChanges();
            return true;
        }

    }
}
