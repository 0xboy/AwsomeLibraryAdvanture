using System;
using System.Data;
using System.Data.SqlClient;

namespace AwsomeLibraryAdvanture.Infrastructure.Core
{
    public class AwsomeDbOperation
    {
        private SqlConnection _conn;

        public AwsomeDbOperation(SqlConnection conn)
        {
            _conn = conn;
        }

        public object spExecuteScalar(string spName, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = _conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            if (parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                try
                {
                    _conn.Open();
                }
                catch (Exception)
                {
                    throw new Exception("Provided connection string is not connectable.");
                }
                object result = cmd.ExecuteScalar();
                _conn.Close();
                return result;
            }


            catch (Exception e)
            {
                _conn.Close();
                return null;
            }


        }

        public bool SpExecute(string spName, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = _conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };

            if (parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            try
            {
                try
                {
                    _conn.Open();
                }
                catch (Exception)
                {

                    throw new Exception("Provided connection string is not connectable.");
                }
                cmd.ExecuteNonQuery();
                _conn.Close();
                return true;
            }

            catch (Exception e)
            {
                _conn.Close();
                return false;
            }
        }


        public SqlDataReader GetData(string spName, SqlParameter[] parameters=null)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = _conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            };
            if (parameters != null)
            {
                if (parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                } 
            }
            try
            {
                _conn.Open();
            }
            catch (Exception)
            {
                throw new Exception("Provided connection string is not connectable.");
            }
            try
            {
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}