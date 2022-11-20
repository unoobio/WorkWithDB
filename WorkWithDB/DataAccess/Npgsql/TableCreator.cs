using Microsoft.Extensions.Logging;
using WorkWithDB.DataAccess.Npgsql.Interfaces;

namespace WorkWithDB.DataAccess.Npgsql
{
    internal class TableCreator : ITableCreator
    {
        private readonly ILogger<TableCreator> _logger;
        private readonly ISqlScriptExecutor _sqlScriptExecutor;

        public TableCreator(ILogger<TableCreator> logger, ISqlScriptExecutor sqlScriptExecutor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sqlScriptExecutor = sqlScriptExecutor ?? throw new ArgumentNullException();
        }

        public void CreateTables()
        {
            CreateTableByScript(SqlScripts.UsersTableCreating, "users");
            CreateTableByScript(SqlScripts.MarketsTableCreating, "markets");
            CreateTableByScript(SqlScripts.ProductsTableCreating, "products");
            CreateTableByScript(SqlScripts.OrdersTableCreating, "orders");
            CreateTableByScript(SqlScripts.OrderProductsTableCreating, "order_products");

        }

        private void CreateTableByScript(string sqlScript, string tableName)
        {
            _sqlScriptExecutor.ExecuteScript(sqlScript);
            _logger.LogInformation("Table '{tableName}' created successfully.", tableName);
        }
    }
}
