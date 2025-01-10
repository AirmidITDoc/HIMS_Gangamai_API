using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Opd;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HIMS.Model.CustomerInformation;


namespace HIMS.Data.Opd
{
    public class R_OPDRegistration :GenericRepository,I_OPDRegistration
    {
        public R_OPDRegistration(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
        public bool TConsentInformationSave(TConsentInformationparams TConsentInformationparams)
        {
            // throw new NotImplementedException();
            var disc = TConsentInformationparams.SaveTConsentInformationparams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_ConsentInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool TConsentInformationUpdate(TConsentInformationparams TConsentInformationparams)
        {
            // throw new NotImplementedException();
            var disc = TConsentInformationparams.UpdateTConsentInformationparams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_ConsentInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool TCertificateInformationSave(TCertificateInformationparams TCertificateInformationparams)
        {
            // throw new NotImplementedException();
            var disc = TCertificateInformationparams.SaveTCertificateInformationparams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_T_CertificateInformation ", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool TCertificateInformationUpdate(TCertificateInformationparams TCertificateInformationparams)
        {
            // throw new NotImplementedException();
            var disc = TCertificateInformationparams.UpdateTCertificateInformationparams.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_CertificateInformation", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }


        public string Insert(OPDRegistrationParams OPDRegistrationParams)
        {
            //add registration
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = OPDRegistrationParams.OPDRegistrationSave.ToDictionary();
            dic.Remove("RegID");
            var RegID = ExecNonQueryProcWithOutSaveChanges("m_insert_Registration_1", dic, outputId);

           _unitofWork.SaveChanges();

            return RegID;
        }

        public bool Update(OPDRegistrationParams OPDRegistrationParams)
        {
            var disc1 = OPDRegistrationParams.OPDRegistrationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_RegForAppointment_1", disc1);

                       /*
            var disc = OPDRegistrationParams.OPDRegistrationUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_Registration_1", disc);*/
            _unitofWork.SaveChanges();
            return true;
            
            
        }

    }
    }
