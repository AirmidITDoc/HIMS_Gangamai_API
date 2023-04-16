
using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System; 
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public class R_ParameterMaster : GenericRepository, I_ParameterMaster
    {
        public R_ParameterMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }


        /*  var dic = PathParameterMasterParams.PathParameterMasterUpdate.ToDictionary();
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
  }*/
        //ISNUMERIC/ISDESCRIPTIVE final code 
        public bool Update(ParameterMasterParams ParameterMasterParams)
        {
            var disc1 = ParameterMasterParams.UpdateParameterMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_update_ParameterMaster_1", disc1);


                if (ParameterMasterParams.UpdateParameterMaster.IsNumeric == 1)
            {

                var D_Det = ParameterMasterParams.DeleteAssignParameterToRange.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("ps_Delete_RangeParameterMaster_1", D_Det); 
                 
                var disc2 = ParameterMasterParams.InsertParameterMasterRangeWise.ToDictionary();
                // disc1.Remove("ParameterId");
                var ParaId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_ParameterRangeMaster_1", disc2);

            }
            else 
            {

                var D_Det = ParameterMasterParams.DeleteAssignParameterToDescriptive.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Delete_DescriptiveParameterMaster_1", D_Det);

                foreach (var a in ParameterMasterParams.InsertAssignParameterToDescriptives)
                {
                    var disc = a.ToDictionary();
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_ParameterDescriptiveMaster_1", disc);
                }
            }

            _unitofWork.SaveChanges();
            return true;
        }
























        /* public bool Update(ParameterMasterParams ParameterMasterParams)
          {
               var disc1 = ParameterMasterParams.UpdateParameterMaster.ToDictionary();
               ExecNonQueryProcWithOutSaveChanges("ps_update_ParameterMaster_1", disc1);

           
            if (ParameterMasterParams.UpdateParameterMaster.IsNumeric == 1)
            {

                var D_Det = ParameterMasterParams.DeleteAssignParameterToRange.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("ps_Delete_RangeParameterMaster_1", D_Det);

                var disc2 = ParameterMasterParams.InsertParameterMasterRangeWise.ToDictionary();
               // disc1.Remove("ParameterId");
                var ParaId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_ParameterRangeMaster_1", disc2);

            }
            else 
            {

                var D_Det = ParameterMasterParams.DeleteAssignParameterToDescriptive.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Delete_DescriptiveParameterMaster_1", D_Det);

                foreach (var a in ParameterMasterParams.InsertAssignParameterToDescriptives)
               {
                   var disc = a.ToDictionary();
                   ExecNonQueryProcWithOutSaveChanges("ps_Insert_ParameterDescriptiveMaster_1", disc);
               }
            }

            _unitofWork.SaveChanges();
               return true;
         }*/

        public bool Insert(ParameterMasterParams ParameterMasterParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ParameterID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = ParameterMasterParams.InsertParameterMaster.ToDictionary();
            disc1.Remove("ParameterID");
            var ParameterID = ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathParameterMaster", disc1, outputId);

           //var ParaId
            //add Range and Descriptive
            if (ParameterMasterParams.InsertParameterMaster.IsNumeric == 1)
            {
                var a = ParameterMasterParams.InsertParameterMasterRangeWise;
                var disc = a.ToDictionary();
                disc["ParaId"] = ParameterID;
                //var disc = ParameterMasterParams.InsertParameterMasterRangeWise.ToDictionary();
                //disc["ParaId"] = ParameterID;
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_ParameterRangeMaster_1", disc);

            }
            else
            {
                foreach (var a in ParameterMasterParams.InsertAssignParameterToDescriptives)
                {
                    var disc = a.ToDictionary();
                    disc["ParameterId"] = ParameterID;
                    ExecNonQueryProcWithOutSaveChanges("ps_Insert_ParameterDescriptiveMaster_1", disc);
                }
            }
            _unitofWork.SaveChanges();
            return true;
        }

        
            /*public bool Update(ParameterMasterParams ParameterMasterParams)
            {
               // var disc1 = ParameterMasterParams.UpdateParameterMaster.ToDictionary();
               //ExecNonQueryProcWithOutSaveChanges("ps_Update_ParameterMaster_1", disc1);
            
                    var disc1 = ParameterMasterParams.UpdateParameterMaster.ToDictionary();
                    var ParaId = disc1;
                    ExecNonQueryProcWithOutSaveChanges("ps_Update_ParameterMaster_1", disc1);

                    if (ParameterMasterParams.UpdateParameterMaster.IsNumeric == 1)
                    {
                                     
                    var D_Det1 = ParameterMasterParams.DeleteAssignParameterToRange.ToDictionary();
                    ExecNonQueryProcWithOutSaveChanges("ps_Delete_RangeParameterMaster_1", D_Det1);


                    if(ParameterMasterParams.UpdateParameterMasterRangeWise.ParaId == ' ' && ParameterMasterParams.UpdateParameterMasterRangeWise.ParaId == 0)
                    {
                    var disc2 = ParameterMasterParams.InsertParameterMasterRangeWise.ToDictionary();
                    // disc2.Remove("ParaId");
                    var ParameterId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_ParameterRangeMaster_1", disc2);

                     }
                     else
                    {
                  
                    var disc2 = ParameterMasterParams.InsertParameterMasterRangeWise.ToDictionary();
                    disc2.Remove("ParaId");
                    var ParameterId = ExecNonQueryProcWithOutSaveChanges("ps_Update_ParameterRangeMaster_1", disc2);

                   }


                 }//main else
             else
                 {

                    var D_Det = ParameterMasterParams.DeleteAssignParameterToDescriptive.ToDictionary();
                    ExecNonQueryProcWithOutSaveChanges("Delete_DescriptiveParameterMaster_1", D_Det);

                    
                     foreach (var a in ParameterMasterParams.InsertAssignParameterToDescriptives)
                    {
                        var disc = a.ToDictionary();
                        ExecNonQueryProcWithOutSaveChanges("ps_Insert_ParameterDescriptiveMaster_1", disc);
                    }

                
            }

                _unitofWork.SaveChanges();
                return true;
            }*/

        

    }

}


