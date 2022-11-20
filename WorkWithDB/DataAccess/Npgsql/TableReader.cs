using Microsoft.Extensions.Logging;
using System.Data;
using WorkWithDB.DataAccess.Npgsql.Interfaces;

namespace WorkWithDB.DataAccess.Npgsql
{
    internal class TableReader : ITableReader
    {
        private readonly ILogger<TableCreator> _logger;
        private readonly ISqlScriptExecutor _sqlScriptExecutor;

        public TableReader(ILogger<TableCreator> logger, ISqlScriptExecutor sqlScriptExecutor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sqlScriptExecutor = sqlScriptExecutor;
        }

        public void ReadTable(string tableName)
        {
            List<Row> rowsValues = _sqlScriptExecutor.GetRowValues(string.Format(SqlScripts.TableReading, tableName));
            _logger.LogInformation($"Rows by '{tableName}':");
            foreach (Row rowValues in rowsValues)
            {
                _logger.LogInformation(string.Join("  ", rowValues.ColumnValues));
            }
        }
    }
}
