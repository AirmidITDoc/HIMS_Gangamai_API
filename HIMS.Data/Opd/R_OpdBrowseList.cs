using HIMS.Common.Extensions;
using HIMS.Model.Opd;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Opd
{
    public class R_OpdBrowseList : I_OpdBrowseList
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public R_OpdBrowseList(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<dynamic> GetBrowseOPDBill(BrowseOPDBillParams browseOPDBillParams)
        {
            command.CommandText = "Retrieve_BrowseOPDBill";
            command.Parameters.AddWithValue("@F_Name", browseOPDBillParams.F_Name);
            command.Parameters.AddWithValue("@L_Name", browseOPDBillParams.L_Name);
            command.Parameters.AddWithValue("@From_Dt", browseOPDBillParams.From_Dt);
            command.Parameters.AddWithValue("@To_Dt", browseOPDBillParams.To_Dt);
            command.Parameters.AddWithValue("@Reg_No", browseOPDBillParams.Reg_No);
            command.Parameters.AddWithValue("@PBillNo", browseOPDBillParams.PBillNo);

            var dataSet = new DataSet();
            (new SqlDataAdapter(command)).Fill(dataSet);
            command.Parameters.Clear();

            return dataSet.Tables[0].ToDynamic();
        }
    }
}
