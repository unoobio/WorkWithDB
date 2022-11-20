using Microsoft.Extensions.Logging;
using WorkWithDB.DataAccess.Npgsql.Interfaces;

namespace WorkWithDB.DataAccess.Npgsql
{
    internal class TablesPlaceholder : ITablesPlaceholder
    {
        private readonly ILogger<TableCreator> _logger;
        private readonly ISqlScriptExecutor _sqlScriptExecutor;

        public TablesPlaceholder(ILogger<TableCreator> logger, ISqlScriptExecutor sqlScriptExecutor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sqlScriptExecutor = sqlScriptExecutor;
        }

        public void InserDefaultRows()
        {
            InsertDefaultRowByScript(SqlScripts.UsersTableInserting, "users");
            InsertDefaultRowByScript(SqlScripts.MarketsTableInserting, "markets");
            InsertDefaultRowByScript(SqlScripts.ProductsTableInserting, "products");
        }

        private void InsertDefaultRowByScript(string sqlScript, string tableName)
        {
            _sqlScriptExecutor.ExecuteScript(sqlScript);
            _logger.LogInformation("Table '{tableName}' filled successfully.", tableName);
        }
    }
}
