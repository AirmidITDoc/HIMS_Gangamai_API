using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Opd;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace HIMS.Data.Opd
{
    public class R_OPDRegistration :GenericRepository,I_OPDRegistration
    {
        public R_OPDRegistration(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

       

        public bool Insert(OPDRegistrationParams OPDRegistrationParams)
        {
            //add registration
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = OPDRegistrationParams.OPDRegistrationSave.ToDictionary();
            dic.Remove("RegId");
            var RegID = ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", dic, outputId);

           _unitofWork.SaveChanges();

            return true;
        }

        public bool Update(OPDRegistrationParams OPDRegistrationParams)
        {
            var disc1 = OPDRegistrationParams.OPDRegistrationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_RegForAppointment_1", disc1);

                       /*
            var disc = OPDRegistrationParams.OPDRegistrationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_Registration_1", disc);*/
            _unitofWork.SaveChanges();
            return true;
            
            
        }

    }
    }
