using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1.Data.Utils
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;


        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.ConnectionString);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }



        public DataTable ExecuteSPQuery(string sp, List<ParameterSP>? param = null)
        {
            DataTable dt = new DataTable();

            try 
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;

                if (param != null)
                {
                    foreach (ParameterSP p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }

                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException)
            {
                dt = null;
            }
            finally { _connection.Close(); }
            return dt;
        }

        public int ExecuteSPNonQuery(string sp, List<ParameterSP> param = null)
        {
            int rowsAffected = 0;

            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.CommandText = sp;
                if (param != null)
                {
                    foreach (ParameterSP p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                rowsAffected= -1;
            }
            finally
            { 
                _connection.Close();
            }

            return rowsAffected;
        }

    }
}
