using Microsoft.Extensions.DependencyInjection;
using System.Data;
using WorkWithDB.DataAccess.Npgsql.Interfaces;

namespace WorkWithDB.DataAccess.Npgsql
{
    public class SqlScriptExecutor : ISqlScriptExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public SqlScriptExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ExecuteScript(string sqlScript, params IDbDataParameter[] sqlParams)
        {
            using (var dbConnection = _serviceProvider.GetRequiredService<IDbConnection>())
            {
                dbConnection.Open();
                using (IDbCommand command = dbConnection.CreateCommand())
                {
                    command.CommandText = sqlScript;
                    foreach (IDbDataParameter sqlParam in sqlParams)
                    {
                        command.Parameters.Add(sqlParam);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Row> GetRowValues(string sqlScript, params IDbDataParameter[] sqlParams)
        {
            using (var dbConnection = _serviceProvider.GetRequiredService<IDbConnection>())
            {
                dbConnection.Open();
                using (IDbCommand command = dbConnection.CreateCommand())
                {
                    command.CommandText = sqlScript;
                    foreach (IDbDataParameter sqlParam in sqlParams)
                    {
                        command.Parameters.Add(sqlParam);
                    }
                    using (IDataReader dataReader = command.ExecuteReader())
                    {
                        List<Row> rows = new List<Row>();
                        while (dataReader.Read())
                        {
                            List<string> columnValues = new List<string>();
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                columnValues.Add(dataReader.GetValue(i)?.ToString() ?? string.Empty);
                            }
                            rows.Add(new Row { ColumnValues = columnValues });
                        }
                        return rows;
                    }
                }
            }
        }
    }
}
