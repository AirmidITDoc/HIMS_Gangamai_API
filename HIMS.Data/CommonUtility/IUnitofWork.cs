using System;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data
{
    public interface IUnitofWork : IDisposable
    {
        public SqlCommand CreateCommand();

        public void SaveChanges();
    }
}
