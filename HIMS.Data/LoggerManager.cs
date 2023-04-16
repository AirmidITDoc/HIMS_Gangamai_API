using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data
{
    public class LoggerManager : ILoggerManager
    {
        private readonly IUnitofWork _unitofWork;
        private readonly SqlCommand command;

        public LoggerManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            command = _unitofWork.CreateCommand();
        }

        public void LogError(string errorMessage)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO LOGGERMANAGER (ErrorMessage) VALUES (@ErrorMessage)";
            command.Parameters.AddWithValue("@ErrorMessage", errorMessage);
            command.ExecuteNonQuery();
        }
    }
}
