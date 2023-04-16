using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HIMS.Data
{
    public interface IGenericRepository
    {
        //by query
        int ExecNonQuery(string query, Dictionary<string, object> entity);
        int ExecNonQueryWithoutPrams(string query);
        //by procedure
        int ExecNonQueryProc(string proc, Dictionary<string, object> entity);
        int ExecNonQueryProcBulk(string proc, JArray entities);

        string ExecNonQueryProcWithOutSaveChanges(string proc, Dictionary<string, object> entity, SqlParameter outputParam = null);

     //   string ExecNonQueryProcWithOutSaveChanges_Select(string proc, Dictionary<string, object> entity, SqlParameter outputParam = null);

        object ExecScalar(string query, Dictionary<string, object> entity);
        object ExecScalarProc(string proc, Dictionary<string, object> entity);
        SqlDataReader ExecDataReader(string query, Dictionary<string, object> entity);
        SqlDataReader ExecDataReaderProc(string proc, Dictionary<string, object> entity);

        List<dynamic> ExecDataSetProc(string proc, Dictionary<string, object> entity);
        List<dynamic> ExecDataSetQuery(string query, Dictionary<string, object> entity);
        List<dynamic> ExecDataSetProcWithDataTable(string proc, JArray entity);
    }
}
