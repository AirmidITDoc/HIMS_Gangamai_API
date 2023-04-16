using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_OTNotesTemplate :GenericRepository,I_OTNotesTemplate
    {
        public R_OTNotesTemplate(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string Insert(OTNotesTemplateparam OTNotesTemplateparam)
        {
            // throw new NotImplementedException();

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OTNoteTempId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc = OTNotesTemplateparam.OTNoteTemplateInsert.ToDictionary();
            disc.Remove("OTNoteTempId");
                
            var Id =ExecNonQueryProcWithOutSaveChanges("insert_M_OTNotesTemplateMaster_1", disc,outputId);

            _unitofWork.SaveChanges();
            return Id;
        }

        public bool Update(OTNotesTemplateparam OTNotesTemplateparam)
        {
          

            var disc1 = OTNotesTemplateparam.OTNoteTemplateUpdate.ToDictionary();
           ExecNonQueryProcWithOutSaveChanges("update_M_OTNotesTemplateMaster_1", disc1);

            _unitofWork.SaveChanges();
            return true;

        }
    }
}
