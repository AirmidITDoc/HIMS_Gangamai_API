using HIMS.Common.Extensions;
using HIMS.Model.Master;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Master
{
    public class R_BillingClassMaster1 : I_BillingClassMaster1
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public R_BillingClassMaster1(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<dynamic> Get(string BillingClassName)
        {
            command.CommandText = "Rtrv_ClassNameList_by_Name";
            command.Parameters.AddWithValue("@ClassName", BillingClassName + "%");
            var dataSet = new DataSet();

            (new SqlDataAdapter(command)).Fill(dataSet);
            command.Parameters.Clear();

            return dataSet.Tables[0].ToDynamic();
        }

        public bool Insert(BillingClassMaster billingclassmaster)
        {
            command.CommandText = "insert_ClassMaster_1";
            command.Parameters.AddWithValue("@ClassName", billingclassmaster.ClassName);
            command.Parameters.AddWithValue("@IsActive", billingclassmaster.IsActive);
            int result = command.ExecuteNonQuery(); // result != 0 then it is saved/updated
            return result != 0;
        }

        public bool Update(BillingClassMaster billingclassmaster)
        {
            command.CommandText = "update_ClassMaster_1";
            command.Parameters.AddWithValue("@ClassId", billingclassmaster.ClassId);
            command.Parameters.AddWithValue("@ClassName", billingclassmaster.ClassName);
            command.Parameters.AddWithValue("@IsActive", billingclassmaster.IsActive);
            int result = command.ExecuteNonQuery(); // result != 0 then it is saved/updated
            return result != 0;
        }
    }
}
