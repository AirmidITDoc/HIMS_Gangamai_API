using HIMS.Common.Utility;
using HIMS.Model.Opd.OP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd.OP
{
  public  class R_SS_RoleTemplateMaster :GenericRepository,I_SS_RoleTemplateMaster
    {
        public R_SS_RoleTemplateMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        
        public bool Save(SS_RoleTemplateMasterParams SS_RoleTemplateMasterParams)
        {
             var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RoleId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RoleDetId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            //Master
            var dic = SS_RoleTemplateMasterParams.SS_RoleTemplateMasterSave.ToDictionary();
            dic.Remove("RoleId");
            var RoleId1=ExecNonQueryProcWithOutSaveChanges("ps_Insert_RoleTemplateMaster_1", dic, outputId);


            //Detail

            foreach (var a in SS_RoleTemplateMasterParams.SS_RoleTemplateDetailSave)
            {
                var disc1 = a.ToDictionary();
                disc1["RoleId"] = RoleId1;
               ExecNonQueryProcWithOutSaveChanges("ps_Insert_RoleTemplateDetail_1", disc1);
            }

            //SS_RoleTemplateMasterParams.SS_RoleTemplateDetailSave.RoleId = Convert.ToInt32(RoleId1);
           // var dic1 = SS_RoleTemplateMasterParams.SS_RoleTemplateDetailSave.ToDictionary();
            //dic1.Remove("RoleDetId");
            //var RoleDetId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_RoleTemplateDetail_1", dic1, outputId1);

            
            //commit transaction
            _unitofWork.SaveChanges();

            return true;
        }

        
        public bool Update(SS_RoleTemplateMasterParams SS_RoleTemplateMasterParams)
        {
            //throw new NotImplementedException();
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RoleDetId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var dic2 = SS_RoleTemplateMasterParams.SS_RoleTemplateMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_RoleTemplateMaster_1", dic2);

            SS_RoleTemplateMasterParams.SS_RoleTemplateDetailDelete.RoleId = SS_RoleTemplateMasterParams.SS_RoleTemplateMasterUpdate.RoleId;
            var dic3 = SS_RoleTemplateMasterParams.SS_RoleTemplateDetailDelete.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Delete_SS_RoleTemplateDetail", dic3);


            
            //var dic4 = SS_RoleTemplateMasterParams.SS_RoleTemplateDetailSave.ToDictionary();
            //dic4["RoleId"] = SS_RoleTemplateMasterParams.SS_RoleTemplateMasterUpdate.RoleId;
            //dic4.Remove("RoleDetId");
            //var RoleDetId= ExecNonQueryProcWithOutSaveChanges("ps_Insert_RoleTemplateDetail_1", dic4,outputId1);

            foreach (var a in SS_RoleTemplateMasterParams.SS_RoleTemplateDetailSave)
            {
                var disc1 = a.ToDictionary();
                //disc1.Remove("RoleId");
               disc1["RoleId"] = SS_RoleTemplateMasterParams.SS_RoleTemplateMasterUpdate.RoleId;
               ExecNonQueryProcWithOutSaveChanges("ps_Insert_RoleTemplateDetail_1", disc1);
            }

            //commit transaction
            _unitofWork.SaveChanges();

            return true;
        }
    }
}
