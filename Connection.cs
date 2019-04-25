using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace ForDataBase
{
    public class ConnectionManager : IDisposable
    {
        protected const string connectionType = "Npgsql";
        protected readonly DbProviderFactory factory = DbProviderFactories.GetFactory(connectionType);
        protected DbConnection connection;
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    connection.Dispose();
                    // Dispose managed resources.
                    //component.Dispose();
                }

                connection.Close();

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.


                // Note disposing has been done.
                disposed = true;
            }
        }


        ~ConnectionManager()
        {
            Dispose(false);
        }


        public ConnectionManager()
        {
            connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["Npgsql"].ConnectionString;
            connection.Open();
        }


        public virtual List<object> GetKeys()//return list of tables
        {
            List<object> tableNames = new List<object>();

            DataTable dt = connection.GetSchema("Tables");

            foreach (DataRow dataRow in dt.Rows)
            {
                tableNames.Add(dataRow["TABLE_NAME"].ToString().Trim());
            }

            return tableNames;
        }


        public void ExecuteNonQuery(string textOfCommand)
        {
            DbCommand command = factory.CreateCommand();

            command.Connection = connection;
            command.CommandText = textOfCommand;

            //connection.Open();

            command.ExecuteNonQuery();
            //connection.Close();
        }


        public object ExecuteScalar(string textOfCommand)
        {
            DbCommand command = factory.CreateCommand();

            command.Connection = connection;
            command.CommandText = textOfCommand;

            //connection.Open();

            object scalar = command.ExecuteScalar();
            //connection.Close();

            return scalar;
        }


        public DbDataReader ExecuteReader(string textOfCommand)
        {
            using (DbCommand command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = textOfCommand;

                //connection.Open();

                DbDataReader reader = command.ExecuteReader();
                ////?
                //connection.Close();
                //command.Cancel();
                //command.Dispose();
                return reader;
            }

        }


        public void RefreshDataReader()
        {
            connection.Dispose();
            connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;
            connection.Open();
        }
    }
}