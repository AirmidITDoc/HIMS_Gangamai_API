using HIMS.Common.Extensions;
using HIMS.Model.Pathology;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Pathology
{
    public class R_PathologyPatientList: I_PathologyPatientList
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public R_PathologyPatientList(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<dynamic> GetPathologyList(PathologyList pathologyList)
        {
            command.CommandText = "Rtrv_PathSamColllist_Pat_Dtls";
            command.Parameters.AddWithValue("@F_Name", pathologyList.F_Name);
            command.Parameters.AddWithValue("@L_Name", pathologyList.L_Name);
            command.Parameters.AddWithValue("@Reg_No", pathologyList.Reg_No);
            command.Parameters.AddWithValue("@From_Dt", pathologyList.From_Dt);
            command.Parameters.AddWithValue("@To_Dt", pathologyList.To_Dt);
            command.Parameters.AddWithValue("@IsCompleted", pathologyList.IsCompleted);
            command.Parameters.AddWithValue("@OP_IP_Type", pathologyList.OP_IP_Type);

            var dataSet = new DataSet();
            (new SqlDataAdapter(command)).Fill(dataSet);
            command.Parameters.Clear();

            return dataSet.Tables[0].ToDynamic();
        }
    }
}
