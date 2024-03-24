using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_CrossConsultation :GenericRepository,I_CrossConsultation
    {
        public R_CrossConsultation(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public string Save(CrossConsultation CrossConsultation)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@VisitID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            //opdAppointmentParams.VisitSave.RegID = Convert.ToInt64(RegID);
            var dic1 = CrossConsultation.CrossConsultationSave.ToDictionary();
            dic1.Remove("VisitID");
            var VisitID = ExecNonQueryProcWithOutSaveChanges("insert_VisitDetails_New_1", dic1, outputId1);

            _unitofWork.SaveChanges();

            return VisitID;

        }
    }
}
