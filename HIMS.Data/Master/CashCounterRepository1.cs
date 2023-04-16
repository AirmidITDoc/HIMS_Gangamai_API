using HIMS.Common.Extensions;
using HIMS.Model.Master;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Master
{
    public class CashCounterRepository1 : ICashCounterRepository
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public CashCounterRepository1(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<dynamic> Get(string cashCounterName)
        {
            command.CommandText = "Rtrv_CashCounterNameList_by_Name";
            command.Parameters.AddWithValue("@CashCounterName", cashCounterName);

            var dataSet = new DataSet();
            (new SqlDataAdapter(command)).Fill(dataSet);
            command.Parameters.Clear();

            return dataSet.Tables[0].ToDynamic();
        }

        public bool Save(CashCounter cashCounter)
        {
            //edit
            var spName = cashCounter.CashCounterId != 0 ? "update" : "insert";
            command.CommandText = $"{spName}_CashCounter_1";

            //edit
            if (cashCounter.CashCounterId != 0)
            {
                command.Parameters.AddWithValue("@CashCounterId", cashCounter.CashCounterId);
            }
            else
            {
                command.Parameters.AddWithValue("@Prefix", cashCounter.Prefix);
                command.Parameters.AddWithValue("@BillNo", cashCounter.BillNo);
            }

            command.Parameters.AddWithValue("@CashCounter", cashCounter.CashCounterName);
            command.Parameters.AddWithValue("@IsActive", cashCounter.IsActive);

            int result = command.ExecuteNonQuery(); // result != 0 then it is saved/updated
            return result != 0;
        }
    }
}
