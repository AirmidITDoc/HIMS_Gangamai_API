using HIMS.Common.Extensions;
using HIMS.Model.Master;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.Master
{
    public class GenericComboRepository : IGenericComboRepository
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public GenericComboRepository(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public List<dynamic> Get(GenericCombo genericCombo)
        {
            command.CommandText = genericCombo.ProcedureName;

            if (!string.IsNullOrWhiteSpace(genericCombo.ParamName))
            {
                command.Parameters.AddWithValue(genericCombo.ParamName, genericCombo.ParamValue);
            }
            var dataSet = new DataSet();
            (new SqlDataAdapter(command)).Fill(dataSet);
            command.Parameters.Clear();

            return dataSet.Tables[0].ToDynamic();
        }
    }
}
