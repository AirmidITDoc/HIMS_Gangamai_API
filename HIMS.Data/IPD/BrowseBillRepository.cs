using HIMS.Common.Extensions;
using HIMS.Model.IPD;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.IPD
{
    public class BrowseBillRepository : IBrowseBillRepository
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public BrowseBillRepository(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<dynamic> GetBrowseIPDBill(GetBrowseIPDBill GetBrowseIPDBill)
        {
            command.CommandText = "Retrieve_BrowseIPDBill";
            command.Parameters.AddWithValue("@F_Name", GetBrowseIPDBill.F_Name);
            command.Parameters.AddWithValue("@L_Name", GetBrowseIPDBill.L_Name);
            command.Parameters.AddWithValue("@From_Dt", GetBrowseIPDBill.From_Dt);
            command.Parameters.AddWithValue("@To_Dt", GetBrowseIPDBill.To_Dt);
            command.Parameters.AddWithValue("@Reg_No", GetBrowseIPDBill.Reg_No);
            command.Parameters.AddWithValue("@PBillNo", GetBrowseIPDBill.PBillNo);
            command.Parameters.AddWithValue("@IsInterimOrFinal", GetBrowseIPDBill.IsInterimOrFinal);
            command.Parameters.AddWithValue("@CompanyId", GetBrowseIPDBill.CompanyId);

            var dataSet = new DataSet();
            (new SqlDataAdapter(command)).Fill(dataSet);
            command.Parameters.Clear();

            return dataSet.Tables[0].ToDynamic();
        }
    }
}
