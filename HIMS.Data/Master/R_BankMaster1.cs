using HIMS.Common.Extensions;
using HIMS.Model.Master;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Master
{
    public class R_BankMaster1 : I_BankMaster1
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public R_BankMaster1(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<dynamic> Get(string BankName) 
        {
            command.CommandText = "Rtrv_BankNameList_by_Name";
            command.Parameters.AddWithValue("@BankName", BankName+"%");
            var dataSet = new DataSet();
            
            (new SqlDataAdapter(command)).Fill(dataSet);
            command.Parameters.Clear();

            return dataSet.Tables[0].ToDynamic();
        }

        public bool Update(BankMaster1 bankmaster)
        {
            command.CommandText = "update_BankMaster_1";
            command.Parameters.AddWithValue("@BankId", bankmaster.BankId);
            command.Parameters.AddWithValue("@BankName", bankmaster.BankName);
            command.Parameters.AddWithValue("@IsDeleted", bankmaster.IsDeleted);
            command.Parameters.AddWithValue("@UpdatedBy", bankmaster.UpdatedBy);
            int result = command.ExecuteNonQuery(); // result != 0 then it is saved/updated
            return result != 0;
        }
        public bool Insert(BankMaster1 bankmaster)
        {
            command.CommandText = "insert_BankName_1";
            command.Parameters.AddWithValue("@BankName", bankmaster.BankName);
            command.Parameters.AddWithValue("@IsDeleted", bankmaster.IsDeleted);
            command.Parameters.AddWithValue("@AddedBy", bankmaster.AddedBy);
            int result = command.ExecuteNonQuery(); // result != 0 then it is saved/updated
            return result != 0;
        }
    }
}
