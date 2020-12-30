using System;
using System.Data;
using System.Data.SqlClient;

namespace AwsomeLibraryAdvanture.Infrastructure.Core
{
    public class AwsomeDbOperation
    {
        private string _connStr;

        public AwsomeDbOperation(string conn)
        {
            _connStr = conn;
        }

        public object spExecuteScalar(string spName, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(_connStr),
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
                    cmd.Connection.Open();
                }
                catch (Exception)
                {
                    throw new Exception("Provided connection string is not connectable.");
                }
                object result = cmd.ExecuteScalar();
                return result;
            }


            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }


        }

        public bool SpExecute(string spName, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(_connStr),
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
                    cmd.Connection.Open();
                }
                catch (Exception)
                {

                    throw new Exception("Provided connection string is not connectable.");
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }

            catch (Exception e)
            {
                throw e;
            }
        }


        public SqlDataReader GetData(string spName, SqlParameter[] parameters=null)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = new SqlConnection(_connStr),
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
                cmd.Connection.Open();
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