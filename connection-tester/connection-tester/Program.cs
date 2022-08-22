using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace connection_tester
{
    class Program
    {
        public enum ExitCode : int
        {
            Success = 1,
            UnknownError = 0
        }

        static void Main(string[] args)
        {
            string connectionString = "Data Source=[ServerName];Initial Catalog=master;Integrated Security=SSPI; persist security info=True";
            string connectionStringOleDb = "Provider=SQLOLEDB;Data Source=[ServerName];Integrated Security=SSPI";

            TestConnectionSQLClient(connectionString);
            TestConnectionOleDb(connectionStringOleDb);
        }

        public static int TestConnectionSQLClient(string connectionString)
        {
            Console.WriteLine("Test connection using SQLClient.");
            Console.WriteLine("Connecting to: {0}", connectionString);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var query = "SELECT 1";
                    Console.WriteLine("Executing {0}", query);

                    var command = new SqlCommand(query, connection);

                    connection.Open();

                    Console.WriteLine("SQL Connection Successful.");

                    command.ExecuteScalar();
                    Console.WriteLine("SQL Query execution successful.");

                    Console.WriteLine("");
                    Console.WriteLine("Enter press a key to continue...");
                    Console.ReadKey();

                    return (int)ExitCode.Success;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Failure: {0]", ex.Message);
                    return (int)ExitCode.UnknownError;
                }
            }
        }

        public static int TestConnectionOleDb(string connectionStringOleDb)
        {
            Console.WriteLine("Test connection using OleDb.");
            Console.WriteLine("Connecting to: {0}", connectionStringOleDb);

            using (OleDbConnection connection = new OleDbConnection(connectionStringOleDb))
            {
                try
                {
                    var query = "SELECT 1";
                    Console.WriteLine("Executing {0}", query);

                    var command = new OleDbCommand(query, connection);

                    connection.Open();

                    Console.WriteLine("SQL Connection Successful.");

                    command.ExecuteScalar();
                    Console.WriteLine("SQL Query execution successful.");

                    Console.WriteLine("");
                    Console.WriteLine("Enter press a key to continue...");
                    Console.ReadKey();
                    return (int)ExitCode.Success;

                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Failure: {0]", ex.Message);
                    return (int)ExitCode.UnknownError;
                }
            }
        }


    }
}
