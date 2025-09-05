using Actividad_1.Data.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Data.Implementation
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private  SqlTransaction _transaction;

        public UnitOfWork()
        {
            _connection = DataHelper.GetInstance().GetConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public SqlConnection Connection => _connection;
        public SqlTransaction Transaction => _transaction;

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}
