using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pathology
{
   public class R_PathParameterMaster : GenericRepository, I_PathParameterMaster
    {
        public R_PathParameterMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }


        public bool Save(PathParameterMasterParams PathParameterMasterParams)
        {
            //PathParametermaster insert
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ParameterID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = PathParameterMasterParams.PathParameterMasterInsert.ToDictionary();
            dic.Remove("ParameterID");
            var ParameterID = ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathParameterMaster", dic, outputId);

            if (PathParameterMasterParams.PathParameterMasterInsert.IsNumeric == true)
            {

                // ParameterRangeWithAgeMaster Insert
                foreach (var a in PathParameterMasterParams.ParameterRangeWithAgeMasterInsert)
                {
                    var d = a.ToDictionary();
                    d["ParaId"] = ParameterID;
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ParameterRangeWithAgeMaster", d);

                }
            }
            else
            {

                // ParameterDescriptiveMaster Insert
                foreach (var a in PathParameterMasterParams.ParameterDescriptiveMasterInsert)
                {
                    var d = a.ToDictionary();
                    d["ParameterID"] = ParameterID;
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ParameterDescriptiveMaster", d);

                }

            }
            _unitofWork.SaveChanges();
                return true;
            
        }

        /*  public bool Save(PathParameterMasterParams PathParameterMasterParams)
          {
              throw new NotImplementedException();
          }

           public bool Save(ParameterMasterAgeWiseParams ParameterMasterAgeWiseParams)
            {
            throw new NotImplementedException();
            }*/

        public bool Update(PathParameterMasterParams PathParameterMasterParams)
            {

                //Update Service
                var dic = PathParameterMasterParams.PathParameterMasterUpdate.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("ps_Update_M_PathParameterMaster", dic);


            if (PathParameterMasterParams.PathParameterMasterUpdate.IsNumeric == true)
            {

                //Delete ParameterRangeWithAgeMaster
                var s1 = PathParameterMasterParams.ParameterRangeWithAgeMasterDelete.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("p_Delete_M_RangeParameterWithAgeMaster", s1);
                //add ServiceDetails
                foreach (var a in PathParameterMasterParams.ParameterRangeWithAgeMasterInsert)
                {
                    var disc = a.ToDictionary();
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ParameterRangeWithAgeMaster", disc);
                }
            }
            else
            {
                var s2 = PathParameterMasterParams.DescriptiveParameterMasterDelete.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("p_Delete_M_DescriptiveParameterMaster", s2);

                //add ServiceDetails
                foreach (var a in PathParameterMasterParams.ParameterDescriptiveMasterInsert)
                {
                    var disc = a.ToDictionary();
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ParameterDescriptiveMaster", disc);
                }
            }

               _unitofWork.SaveChanges();
                return true;

            }

       
    }
    }
