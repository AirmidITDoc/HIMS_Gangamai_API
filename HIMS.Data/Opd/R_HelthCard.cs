using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_HealthCard : GenericRepository, I_HelthCard
    {
        public R_HealthCard(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public String Insert(HelthCardParam InsertHelthCardParam)
        {
     
       
            var disc = InsertHelthCardParam.InsertHelthCardParam.ToDictionary();
            //disc.Remove("");
            var VisitId = ExecNonQueryProcWithOutSaveChanges("m_Insert_HealthCard", disc);
            
            

            _unitofWork.SaveChanges();
            return (VisitId);
        }



    }
}
