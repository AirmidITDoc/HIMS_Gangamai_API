using HIMS.Common.Utility;
using HIMS.Model.Administration;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_Mrdmedicalcertificate : GenericRepository, I_Mrdmedicalcertificate
    {
        public R_Mrdmedicalcertificate(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }
        public string InsertPatICDCode(PatICDCodeParam PatICDCodeParam)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PatICDCodeId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            // throw new NotImplementedException();
            var disc = PatICDCodeParam.InsertPatICDCodeParamHeader.ToDictionary();
            disc.Remove("PatICDCodeId");
            var HId = ExecNonQueryProcWithOutSaveChanges("m_Insert_T_PatICDCode_Header", disc, outputId1);

            foreach (var a in PatICDCodeParam.InsertPatICDCodeParamDetails)
            {
                var disc2 = a.ToDictionary();
                disc2.Remove("DId");
                disc["PatICDCodeId"] = HId;
               var DId =ExecNonQueryProcWithOutSaveChanges("M_Insert_T_PatICDCode_Details", disc2, outputId2);
            }


            _unitofWork.SaveChanges();


            return HId;
        }



        public bool UpdatePatICDCode(PatICDCodeParams PatICDCodeParam)
        {

            // delete previous data from  table
            var vVisitId = PatICDCodeParam.DeletePatICDCodeParamHeader.ToDictionary();
            vVisitId["HId"] = PatICDCodeParam.UpdatePatICDCodeParamDetails;
            ExecNonQueryProcWithOutSaveChanges("m_delete_T_PatICDCode", vVisitId);
            // throw new NotImplementedException();
            var disc = PatICDCodeParam.UpdatePatICDCodeParamHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("M_Update_T_PatICDCode_Header", disc);


            foreach (var a in PatICDCodeParam.UpdatePatICDCodeParamDetails)
            {
                var disc2 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("M_Update_T_PatICDCode_Details", disc2);
            }
            // update follow 


            _unitofWork.SaveChanges();

            return true;
        }

        public bool InsertMrdMedicolegalCertificate(MrdMedicolegalCertificateparam MrdMedicolegalCertificateparam)
        {
            // throw new NotImplementedException();
            var disc = MrdMedicolegalCertificateparam.InsertMrdMedicolegalCertificate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_MedicolegalCertificate_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
        public bool UpdateMrdMedicolegalCertificate(MrdMedicolegalCertificateparam MrdMedicolegalCertificateparam)
        {
            // throw new NotImplementedException();
            var disc = MrdMedicolegalCertificateparam.UpdateMrdMedicolegalCertificate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_MedicolegalCertificate_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Insert(Mrdmedicalcertificateparam Mrdmedicalcertificateparam)
        {
            //  throw new NotImplementedException();

            var dic = Mrdmedicalcertificateparam.InsertMrdmedicalcertificate.ToDictionary();
            dic.Remove("MLCId");
            ExecNonQueryProcWithOutSaveChanges("insert_MLCInfo_1", dic);


            _unitofWork.SaveChanges();
            return true;
        }

     
        public bool Update(Mrdmedicalcertificateparam Mrdmedicalcertificateparam)
        {
            // throw new NotImplementedException();

            var disc1 = Mrdmedicalcertificateparam.UpdateMrdmedicalcertificate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_MLCInfo_1", disc1);

            _unitofWork.SaveChanges();
            return true;
        }
        public bool SaveMICDCodingMaster(MICDCodingMasterParam MICDCodingMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MICDCodingMasterParam.SaveMICDCodingMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_ICDCodingMaster_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMICDCodingMaster(MICDCodingMasterParam MICDCodingMasterParam)
        {

            var disc3 = MICDCodingMasterParam.UpdateMICDCodingMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_ICDCodingMaster_1", disc3);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool SaveMICDCdeheadMaster(MICDCdeheadMasterParam MICDCdeheadMasterParam)
        {
            // throw new NotImplementedException();
            var disc = MICDCdeheadMasterParam.SaveMICDCdeheadMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_M_ICDCdeheadMaster_1", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
        public bool UpdateMICDCdeheadMaster(MICDCdeheadMasterParam MICDCdeheadMasterParam)
        {

            var disc3 = MICDCdeheadMasterParam.UpdateMICDCdeheadMasterParam.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_M_ICDCdeheadMaster_1", disc3);

            _unitofWork.SaveChanges();
            return true;
        }


    }
}
